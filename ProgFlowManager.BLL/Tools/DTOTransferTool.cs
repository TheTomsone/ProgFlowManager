using ProgFlowManager.DAL;
using ProgFlowManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Tools
{
    public static class DTOTransferTool
    {
        public static TModelDTO ToDTO<TModelDTO, TModel>(this TModel model)
            where TModelDTO : IModel
            where TModel : IModel
        {
            return model.ConvertTo<TModelDTO, TModel>();
        }
        public static IEnumerable<TModelDTO> ToDTO<TModelDTO, TModel>(this IEnumerable<TModel> models)
            where TModelDTO : IModel
            where TModel : IModel
        {
            return models.ConvertTo<TModelDTO, TModel>();
        }

        public static IEnumerable<TModel> MergeOne<TModel, TRelated, TBaseRelated, TBaseModel>(
            this IEnumerable<TModel> source,
            Func<int, TBaseModel> baseModelSelector,
            Func<TBaseModel, int> baseModelToRelatedId,
            Func<int, TBaseRelated> baseRelatedSelector)
            //Func<TModel, TBaseRelated> relatedSelector,
            //Func<int, TBaseRelated> relatedFetcher)
            where TModel : IModel
            where TRelated : IModel, new()
            where TBaseRelated : IModel
            where TBaseModel : IModel
        {
            //Console.WriteLine($"\n\n\nMergeOne<TModel : {typeof(TModel).Name}, TRelated : {typeof(TRelated).Name}, TRelatedModel : {typeof(TRelatedModel).Name}> :");

            foreach (TModel item in source)
            {
                int relatedId = baseModelToRelatedId(baseModelSelector(item.Id));
                TRelated related = baseRelatedSelector(relatedId).ToDTO<TRelated, TBaseRelated>();
                PropertyInfo prop = typeof(TModel).GetProperty(typeof(TBaseRelated).Name);

                if (prop is not null && prop.CanWrite) prop.SetValue(item, related);


                //TRelated related = relatedSelector(item);

                //Console.WriteLine("\nBefore :");
                //foreach (PropertyInfo prop in typeof(TModel).GetProperties())
                //    Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(item)}");
                //Console.WriteLine($"{typeof(TRelated).Name} - int Id : {related.Id}");

                //item.UpdateRelated(relatedFetcher(related.Id).ToDTO<TRelated, TBaseRelated>());

                //Console.WriteLine("\nAfter :");
                //foreach (PropertyInfo prop in typeof(TModel).GetProperties())
                //    Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(item)}");
                //Console.WriteLine($"{typeof(TRelated).Name} - int Id : {related.Id}");

                yield return item;
            }
        }
        public static IEnumerable<TModel> MergeManyToOne<TModel, TRelated, TRelatedModel>(
            this IEnumerable<TModel> source,
            Func<TModel, List<TRelated>> relatedEnumerableSelector,
            Func<int, List<TRelatedModel>> relatedFetcher)
            where TModel : IModel
            where TRelated : IModel, new()
            where TRelatedModel : IModel
        {
            Console.WriteLine($"\n\n\nMergeManyToOne<TModel : {typeof(TModel).Name}, TRelated : {typeof(TRelated).Name}, TRelatedModel : {typeof(TRelatedModel).Name}> :");
            foreach (TModel item in source)
            {
                List<TRelated> relateds = relatedEnumerableSelector(item);
                List<TRelatedModel> relatedModels = relatedFetcher(item.Id);

                Console.WriteLine("\nBefore :");
                foreach (PropertyInfo prop in typeof(TModel).GetProperties())
                    Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(item)}");
                Console.WriteLine($"{typeof(TRelated).Name} relateds : ");
                foreach (TRelated related in relateds)
                    foreach (PropertyInfo prop in typeof(TRelated).GetProperties())
                        Console.WriteLine($"{typeof(TRelated).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(related)}");
                Console.WriteLine($"{typeof(TRelated).Name} relatedModels : ");
                foreach (TRelatedModel relatedModel in relatedModels)
                    foreach (PropertyInfo prop in typeof(TRelatedModel).GetProperties())
                        Console.WriteLine($"{typeof(TRelatedModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(relatedModel)}");

                foreach (TRelatedModel relatedModel in relatedModels)
                    relateds.Add(relatedModel.ToDTO<TRelated, TRelatedModel>());

                Console.WriteLine("\nAfter :");
                foreach (PropertyInfo prop in typeof(TModel).GetProperties())
                    Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(item)}");
                Console.WriteLine($"{typeof(TRelated).Name} relateds : ");
                foreach (TRelated related in relateds)
                    foreach (PropertyInfo prop in typeof(TRelated).GetProperties())
                        Console.WriteLine($"{typeof(TRelated).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(related)}");
                Console.WriteLine($"{typeof(TRelated).Name} sources : ");
                foreach (TRelated related in relatedEnumerableSelector(item))
                    foreach (PropertyInfo prop in typeof(TRelated).GetProperties())
                        Console.WriteLine($"{typeof(TRelated).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(related)}");

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
        public static IEnumerable<TModel> MergeManyToMany<TModel, TRelated, TRelation, TRelatedModel>(
            this IEnumerable<TModel> source,
            Func<TModel, List<TRelated>> relatedEnumerableSelector,
            Func<int, IEnumerable<TRelation>> relationFetcher,
            Func<TRelation, int> relationToRelatedId,
            Func<int, TRelatedModel> relatedFetcher)
            where TModel : IModel
            where TRelated : IModel, new()
            where TRelatedModel : IModel
        {
            Console.WriteLine($"\n\n\nMergeManyToMany<TModel : {typeof(TModel).Name}, TRelated : {typeof(TRelated).Name}, TRelation : {typeof(TRelation).Name}, TRelatedModel : {typeof(TRelatedModel).Name}> :");
            foreach (TModel item in source)
            {
                List<TRelated> relateds = relatedEnumerableSelector(item);
                IEnumerable<TRelation> relations = relationFetcher(item.Id);

                Console.WriteLine("\nBefore :");
                foreach (PropertyInfo prop in typeof(TModel).GetProperties())
                    Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(item)}");
                Console.WriteLine($"{typeof(TRelated).Name} relateds : ");
                foreach (TRelated related in relateds)
                    foreach (PropertyInfo prop in typeof(TRelated).GetProperties())
                        Console.WriteLine($"{typeof(TRelated).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(related)}");
                Console.WriteLine($"{typeof(TRelated).Name} relations : ");
                foreach (TRelation relation in relations)
                    foreach (PropertyInfo prop in typeof(TRelation).GetProperties())
                        Console.WriteLine($"{typeof(TRelation).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(relation)}");

                foreach (TRelation relation in relations)
                {
                    int relatedId = relationToRelatedId(relation);
                    //Console.WriteLine($"Int (Func relationToRelatedId) : {relatedId} - TRelatedModel (DAL) : {relatedFetcher(relatedId).Id} - TRelated (DTO) {relatedFetcher(relatedId).ToDTO<TRelated, TRelatedModel>().Id}");
                    relateds.Add(relatedFetcher(relatedId).ToDTO<TRelated, TRelatedModel>());
                }

                Console.WriteLine("\nBefore :");
                foreach (PropertyInfo prop in typeof(TModel).GetProperties())
                    Console.WriteLine($"{typeof(TModel).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(item)}");
                Console.WriteLine($"{typeof(TRelated).Name} relateds : ");
                foreach (TRelated related in relateds)
                    foreach (PropertyInfo prop in typeof(TRelated).GetProperties())
                        Console.WriteLine($"{typeof(TRelated).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(related)}");
                Console.WriteLine($"{typeof(TRelated).Name} sources : ");
                foreach (TRelated related in relatedEnumerableSelector(item))
                    foreach (PropertyInfo prop in typeof(TRelated).GetProperties())
                        Console.WriteLine($"{typeof(TRelated).Name} - {prop.PropertyType} {prop.Name} : {prop.GetValue(related)}");

                yield return item;
            }
        }
    }
}
