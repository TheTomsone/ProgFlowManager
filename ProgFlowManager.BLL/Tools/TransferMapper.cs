using ProgFlowManager.BLL.Interfaces;
using ProgFlowManager.BLL.Models;
using ProgFlowManager.BLL.Models.Programs;
using ProgFlowManager.DAL.Interfaces;
using ProgFlowManager.DAL.Interfaces.Programs;
using ProgFlowManager.DAL.Models;
using ProgFlowManager.DAL.Models.Programs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Tools
{
    public static class TransferMapper
    {
        public static TModel ToModel<TModel, TModelForm>(this TModelForm form, int id)
            where TModel : class, IModelDAL, new()
            where TModelForm : class
        {
            TModel model = new()
            {
                Id = id
            };

            return form.ToModel(model);
        }
        public static TModel ToModel<TModel, TModelForm>(this TModelForm form, TModel? model = null)
            where TModel : class, new()
            where TModelForm : class
        {
            PropertyInfo[] props = typeof(TModel).GetProperties();
            model ??= new();

            foreach (PropertyInfo prop in props)
            {
                if (prop.Name == "Id") continue;

                PropertyInfo? formProp = typeof(TModelForm).GetProperty(prop.Name);

                if (formProp is not null && prop.PropertyType == formProp.PropertyType)
                {
                    object? value = formProp.GetValue(form);
                    if (value is not null) prop.SetValue(model, value);
                }
            }

            return model;
        }
        public static TModelDTO ToDTO<TModelDTO, TModel>(this TModel model)
            where TModelDTO : IModelDTO, new()
            where TModel : IModelDAL
        {
            TModelDTO dto = new();
            PropertyInfo[] props = typeof(TModelDTO).GetProperties();
            object? value;

            foreach (PropertyInfo prop in props)
            {
                PropertyInfo modelProp = typeof(TModel).GetProperty(prop.Name);
                if (modelProp is not null && modelProp.PropertyType == prop.PropertyType)
                {
                    value = modelProp.GetValue(model);
                    if (value is not null) prop.SetValue(dto, value);
                }

            }

            return dto;
        }
        public static IEnumerable<TModelDTO> ToDTO<TModelDTO, TModel>(this IEnumerable<TModel> models)
            where TModelDTO : IModelDTO, new()
            where TModel : IModelDAL
        {
            foreach (TModel model in models)
                yield return model.ToDTO<TModelDTO, TModel>();
        }

        public static IEnumerable<TModel> MergeWith<TModel>(this IEnumerable<TModel> source, IEnumerable<TModel> other)
            where TModel : IModelDTO
        {
            if (source is null) throw new ArgumentNullException("source");
            if (other is null) throw new ArgumentNullException("other");

            PropertyInfo[] props = typeof(TModel).GetProperties();

            foreach (TModel item in source)
            {
                TModel otherItem = other.First(o => o.Id == item.Id);

                foreach (PropertyInfo prop in props)
                {
                    if (!prop.CanRead) continue;

                    object value = prop.GetValue(otherItem);

                    if (!prop.CanWrite) continue;
                    if (value is not null) prop.SetValue(item, value);
                }

                yield return item;
            }
        }

        /// <summary>
        /// This function takes a list of source models of type <typeparamref name="TModel" /> and merges related list of type <typeparamref name="TRelated" /> with an association of type <typeparamref name="TRelation" /> with the base model of type <typeparamref name="TRelatedModel" />.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TRelated"></typeparam>
        /// <typeparam name="TRelation"></typeparam>
        /// <typeparam name="TRelatedModel"></typeparam>
        /// <param name="source"></param>
        /// <param name="relatedEnumerableSelector"></param>
        /// <param name="relationFetcher"></param>
        /// <param name="relationToRelatedId"></param>
        /// <param name="relatedFetcher"></param>
        /// <returns></returns>
        public static IEnumerable<TModel> MergeData<TModel, TRelated, TRelation, TRelatedModel>(
            this IEnumerable<TModel> source,
            Func<TModel, List<TRelated>> relatedEnumerableSelector,
            Func<int, IEnumerable<TRelation>> relationFetcher,
            Func<TRelation, int> relationToRelatedId,
            Func<int, TRelatedModel> relatedFetcher)
            where TModel : IModelDTO
            where TRelated : IModelDTO, new()
            where TRelatedModel : IModelDAL
        {
            foreach (TModel item in source)
            {
                List<TRelated> relateds = relatedEnumerableSelector(item);
                IEnumerable<TRelation> relations = relationFetcher(item.Id);

                foreach (TRelation relation in relations)
                {
                    int relatedId = relationToRelatedId(relation);
                    relateds.Add(relatedFetcher(relatedId).ToDTO<TRelated, TRelatedModel>());
                }

                yield return item;
            }
        }
    }
}




/// <summary>
/// Merges related associations into a list of enriched source models.
/// </summary>
/// <remarks>
/// <para>
/// This function takes a list of source models of type <typeparamref name="TModel" /> and merges related associations with these models.
/// Relations are extracted using the selection function <paramref name="relatedEnumerableSelector" />,
/// then retrieved and enriched from other sources using the specified functions.
/// </para>
/// <para>
/// The function iterates through the list of source models, and for each source model, it extracts related associations using the
/// <paramref name="relatedEnumerableSelector" /> function. It then retrieves related associations using the
/// <paramref name="relationFetcher" /> function, identifies associated model objects from these relations using the
/// <paramref name="relationToRelatedId" /> function, and finally enriches the source models with these model objects using
/// the <paramref name="relatedFetcher" /> function.
/// </para>
/// <para>
/// The function takes into account the following generic constraints:
/// - <typeparam name="TModel">: The type of the source model, which must implement the <see cref="IModelDTO" /> interface.
/// - <typeparam name="TRelated">: The type of the relation model, which must implement the <see cref="IModelDTO" /> interface
///   and must have a parameterless default constructor.
/// - <typeparam name="TRelation">: The type of the relation, which will be used to extract relations.
/// - <typeparam name="TRelatedModel">: The type of the model associated with relations, which must implement the <see cref="IModelDAL" /> interface.
/// </para>
/// <para>
/// The result of the function is a list of enriched models of type <typeparamref name="TModel" /> with their associated relations.
/// Each source model in the resulting list will now have enriched relations in their corresponding properties.
/// </para>
/// </remarks>
/// <typeparam name="TModel">The type of source model.</typeparam>
/// <typeparam name="TRelated">The type of relation model.</typeparam>
/// <typeparam name="TRelation">The type of relation.</typeparam>
/// <typeparam name="TRelatedModel">The type of the model associated with relations.</typeparam>
/// <param name="source">The list of source models from which relations will be merged.</param>
/// <param name="relatedEnumerableSelector">
///     A function that extracts related associations associated with a source model.
/// </param>
/// <param name="relationFetcher">
///     A function to retrieve related associations based on the identifier of the source model.
/// </param>
/// <param name="relationToRelatedId">
///     A function that extracts the identifier of the relation from the relation object.
/// </param>
/// <param name="relatedFetcher">
///     A function to retrieve the associated model object with relations based on its identifier.
/// </param>
/// <returns>
/// A list of enriched models with their associated relations.
/// </returns>
