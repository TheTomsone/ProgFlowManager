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
        //public static TModel ToModel<TModel, TModelForm>(this TModelForm form, int id)
        //    where TModel : class, IModelDAL, new()
        //    where TModelForm : class
        //{
        //    TModel model = new()
        //    {
        //        Id = id
        //    };

        //    return form.ToModel(model);
        //}
        //public static TModel ToModel<TModel, TModelForm>(this TModelForm form, TModel? model = null)
        //    where TModel : class, new()
        //    where TModelForm : class
        //{
        //    PropertyInfo[] props = typeof(TModel).GetProperties();
        //    model ??= new();

        //    foreach (PropertyInfo prop in props)
        //    {
        //        if (prop.Name == "Id") continue;

        //        PropertyInfo? formProp = typeof(TModelForm).GetProperty(prop.Name);

        //        if (formProp is not null && prop.PropertyType == formProp.PropertyType)
        //        {
        //            object? value = formProp.GetValue(form);
        //            if (value is not null) prop.SetValue(model, value);
        //        }
        //    }

        //    return model;
        //}
        //public static TModelDTO ToDTO<TModelDTO, TModel>(this TModel model)
        //    where TModelDTO : IModelDTO, new()
        //    where TModel : IModelDAL
        //{
        //    TModelDTO dto = new();
        //    PropertyInfo[] props = typeof(TModelDTO).GetProperties();

        //    foreach (PropertyInfo prop in props)
        //    {
        //        PropertyInfo modelProp = typeof(TModel).GetProperty(prop.Name);
        //        if (modelProp is not null && modelProp.PropertyType == prop.PropertyType)
        //        {
        //            object value = modelProp.GetValue(model);
        //            if (value is not null) prop.SetValue(dto, value);
        //        }

        //    }

        //    return dto;
        //}
        //public static IEnumerable<TModelDTO> ToDTO<TModelDTO, TModel>(this IEnumerable<TModel> models)
        //    where TModelDTO : IModelDTO, new()
        //    where TModel : IModelDAL
        //{
        //    foreach (TModel model in models)
        //        yield return model.ToDTO<TModelDTO, TModel>();
        //}

        //public static IEnumerable<TModel> ConvertTo<TModel, TBaseModel>(this IEnumerable<TBaseModel> baseModels)
        //    where TModel : new()
        //{
        //    PropertyInfo[] props = typeof(TModel).GetProperties();

        //    Console.WriteLine($"\n\n\nConvertTo<{typeof(TModel).Name}, {typeof(TBaseModel).Name}> :");

        //    foreach (TBaseModel item in baseModels)
        //    {
        //        TModel model = new();
        //        foreach (PropertyInfo prop in props)
        //        {
        //            PropertyInfo modelProp = typeof(TBaseModel).GetProperty(prop.Name);

        //            Console.WriteLine("\nBefore :");
        //            Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} = {prop.GetValue(model)}");
        //            if (modelProp is not null) Console.WriteLine($"{typeof(TBaseModel).Name} - {modelProp.PropertyType} {modelProp.Name} = {modelProp.GetValue(item)}");
        //            else Console.WriteLine($"{typeof(TBaseModel).Name} do not contain property {prop.Name}");

        //            if (modelProp is not null && modelProp.PropertyType == prop.PropertyType)
        //            {
        //                object? value = modelProp.GetValue(item);
        //                if (value is not null) prop.SetValue(model, value);
        //            }

        //            Console.WriteLine("\nAfter :");
        //            Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} = {prop.GetValue(model)}");
        //            if (modelProp is not null) Console.WriteLine($"{typeof(TBaseModel).Name} - {modelProp.PropertyType} {modelProp.Name} = {modelProp.GetValue(item)}");
        //            else Console.WriteLine($"{typeof(TBaseModel).Name} do not contain property {prop.Name}");

        //        }

        //        yield return model;
        //    }
        //}
        //public static IEnumerable<TModel> MergeOne<TModel, TRelated, TBaseRelated, TBaseModel>(
        //    this IEnumerable<TModel> source,
        //    Func<int, TBaseModel> baseModelSelector,
        //    Func<TBaseModel, int> baseModelToRelatedId,
        //    Func<int ,TBaseRelated> baseRelatedSelector)
        //    //Func<TModel, TBaseRelated> relatedSelector,
        //    //Func<int, TBaseRelated> relatedFetcher)
        //    where TModel : IModelDTO
        //    where TRelated : IModelDTO, new()
        //    where TBaseRelated : IModelDAL
        //    where TBaseModel : IModelDAL
        //{
        //    //Console.WriteLine($"\n\n\nMergeOne<TModel : {typeof(TModel).Name}, TRelated : {typeof(TRelated).Name}, TRelatedModel : {typeof(TRelatedModel).Name}> :");

        //    foreach (TModel item in source)
        //    {
        //        int relatedId = baseModelToRelatedId(baseModelSelector(item.Id));
        //        TRelated related = baseRelatedSelector(relatedId).ToDTO<TRelated, TBaseRelated>();
        //        PropertyInfo prop = typeof(TModel).GetProperty(typeof(TBaseRelated).Name);

        //        if (prop is not null && prop.CanWrite) prop.SetValue(item, related);


        //        //TRelated related = relatedSelector(item);

        //        //Console.WriteLine("\nBefore :");
        //        //foreach (PropertyInfo prop in typeof(TModel).GetProperties())
        //        //    Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(item)}");
        //        //Console.WriteLine($"{typeof(TRelated).Name} - int Id : {related.Id}");

        //        //item.UpdateRelated(relatedFetcher(related.Id).ToDTO<TRelated, TBaseRelated>());

        //        //Console.WriteLine("\nAfter :");
        //        //foreach (PropertyInfo prop in typeof(TModel).GetProperties())
        //        //    Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(item)}");
        //        //Console.WriteLine($"{typeof(TRelated).Name} - int Id : {related.Id}");

        //        yield return item;
        //    }
        //}
        //public static IEnumerable<TModel> MergeManyToOne<TModel, TRelated, TRelatedModel>(
        //    this IEnumerable<TModel> source,
        //    Func<TModel, List<TRelated>> relatedEnumerableSelector,
        //    Func<int, List<TRelatedModel>> relatedFetcher)
        //    where TModel : IModelDTO
        //    where TRelated : IModelDTO, new()
        //    where TRelatedModel : IModelDAL
        //{
        //    Console.WriteLine($"\n\n\nMergeManyToOne<TModel : {typeof(TModel).Name}, TRelated : {typeof(TRelated).Name}, TRelatedModel : {typeof(TRelatedModel).Name}> :");
        //    foreach (TModel item in source)
        //    {
        //        List<TRelated> relateds = relatedEnumerableSelector(item);
        //        List<TRelatedModel> relatedModels = relatedFetcher(item.Id);

        //        Console.WriteLine("\nBefore :");
        //        foreach (PropertyInfo prop in typeof(TModel).GetProperties())
        //            Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(item)}");
        //        Console.WriteLine($"{typeof(TRelated).Name} relateds : ");
        //        foreach (TRelated related in relateds)
        //            foreach (PropertyInfo prop in typeof(TRelated).GetProperties())
        //                Console.WriteLine($"{   typeof(TRelated).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(related)}");
        //        Console.WriteLine($"{typeof(TRelated).Name} relatedModels : ");
        //        foreach (TRelatedModel relatedModel in relatedModels)
        //            foreach (PropertyInfo prop in typeof(TRelatedModel).GetProperties())
        //                Console.WriteLine($"{typeof(TRelatedModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(relatedModel)}");

        //        foreach (TRelatedModel relatedModel in relatedModels)
        //            relateds.Add(relatedModel.ToDTO<TRelated, TRelatedModel>());

        //        Console.WriteLine("\nAfter :");
        //        foreach (PropertyInfo prop in typeof(TModel).GetProperties())
        //            Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(item)}");
        //        Console.WriteLine($"{typeof(TRelated).Name} relateds : ");
        //        foreach (TRelated related in relateds)
        //            foreach (PropertyInfo prop in typeof(TRelated).GetProperties())
        //                Console.WriteLine($"{typeof(TRelated).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(related)}");
        //        Console.WriteLine($"{typeof(TRelated).Name} sources : ");
        //        foreach (TRelated related in relatedEnumerableSelector(item))
        //            foreach (PropertyInfo prop in typeof(TRelated).GetProperties())
        //                Console.WriteLine($"{typeof(TRelated).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(related)}");

        //        yield return item;
        //    }
        //}

        ///// <summary>
        ///// This function takes a list of source models of type <typeparamref name="TModel" /> and merges related list of type <typeparamref name="TRelated" /> with an association of type <typeparamref name="TRelation" /> with the base model of type <typeparamref name="TRelatedModel" />.
        ///// </summary>
        ///// <typeparam name="TModel"></typeparam>
        ///// <typeparam name="TRelated"></typeparam>
        ///// <typeparam name="TRelation"></typeparam>
        ///// <typeparam name="TRelatedModel"></typeparam>
        ///// <param name="source"></param>
        ///// <param name="relatedEnumerableSelector"></param>
        ///// <param name="relationFetcher"></param>
        ///// <param name="relationToRelatedId"></param>
        ///// <param name="relatedFetcher"></param>
        ///// <returns></returns>
        //public static IEnumerable<TModel> MergeManyToMany<TModel, TRelated, TRelation, TRelatedModel>(
        //    this IEnumerable<TModel> source,
        //    Func<TModel, List<TRelated>> relatedEnumerableSelector,
        //    Func<int, IEnumerable<TRelation>> relationFetcher,
        //    Func<TRelation, int> relationToRelatedId,
        //    Func<int, TRelatedModel> relatedFetcher)
        //    where TModel : IModelDTO
        //    where TRelated : IModelDTO, new()
        //    where TRelatedModel : IModelDAL
        //{
        //    Console.WriteLine($"\n\n\nMergeManyToMany<TModel : {typeof(TModel).Name}, TRelated : {typeof(TRelated).Name}, TRelation : {typeof(TRelation).Name}, TRelatedModel : {typeof(TRelatedModel).Name}> :");
        //    foreach (TModel item in source)
        //    {
        //        List<TRelated> relateds = relatedEnumerableSelector(item);
        //        IEnumerable<TRelation> relations = relationFetcher(item.Id);

        //        Console.WriteLine("\nBefore :");
        //        foreach (PropertyInfo prop in typeof(TModel).GetProperties())
        //            Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(item)}");
        //        Console.WriteLine($"{typeof(TRelated).Name} relateds : ");
        //        foreach (TRelated related in relateds)
        //            foreach (PropertyInfo prop in typeof(TRelated).GetProperties())
        //                Console.WriteLine($"{typeof(TRelated).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(related)}");
        //        Console.WriteLine($"{typeof(TRelated).Name} relations : ");
        //        foreach (TRelation relation in relations)
        //            foreach (PropertyInfo prop in typeof(TRelation).GetProperties())
        //                Console.WriteLine($"{typeof(TRelation).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(relation)}");

        //        foreach (TRelation relation in relations)
        //        {
        //            int relatedId = relationToRelatedId(relation);
        //            //Console.WriteLine($"Int (Func relationToRelatedId) : {relatedId} - TRelatedModel (DAL) : {relatedFetcher(relatedId).Id} - TRelated (DTO) {relatedFetcher(relatedId).ToDTO<TRelated, TRelatedModel>().Id}");
        //            relateds.Add(relatedFetcher(relatedId).ToDTO<TRelated, TRelatedModel>());
        //        }

        //        Console.WriteLine("\nBefore :");
        //        foreach (PropertyInfo prop in typeof(TModel).GetProperties())
        //            Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(item)}");
        //        Console.WriteLine($"{typeof(TRelated).Name} relateds : ");
        //        foreach (TRelated related in relateds)
        //            foreach (PropertyInfo prop in typeof(TRelated).GetProperties())
        //                Console.WriteLine($"{typeof(TRelated).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(related)}");
        //        Console.WriteLine($"{typeof(TRelated).Name} sources : ");
        //        foreach (TRelated related in relatedEnumerableSelector(item))
        //            foreach (PropertyInfo prop in typeof(TRelated).GetProperties())
        //                Console.WriteLine($"{typeof(TRelated).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(related)}");

        //        yield return item;
        //    }
        //}
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
/// - <typeparam name="TRelatedModel">: The type of the model associated with relations, which must implement the <see cref="IModel" /> interface.
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
