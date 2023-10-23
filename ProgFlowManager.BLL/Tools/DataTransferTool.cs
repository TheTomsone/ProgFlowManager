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
    public static class DataTransferTool
    {
        private static TModel TransferLogicFrom<TModel, TOther>(this TModel source, TOther other, bool isMerge = false, bool isNew = false)
        {
            if (source is null) throw new ArgumentNullException($"{typeof(TModel).Name} source can't be null");
            if (other is null) throw new ArgumentNullException($"{typeof(TOther).Name} other can't be null");

            PropertyInfo[] props = typeof(TModel).GetProperties();

            foreach (PropertyInfo prop in props)
            {
                PropertyInfo? otherProp = typeof(TOther).GetProperty(prop.Name);

                if (isNew && prop.Name == "Id") continue;
                if (otherProp is null) continue;
                if (!prop.CanRead && !prop.CanWrite) continue;
                if (isMerge && prop.GetValue(source) is not null) continue;
                if (otherProp.CanRead && prop.PropertyType == otherProp.PropertyType)
                    prop.SetValue(source, otherProp.GetValue(other));
            }

            return source;
        }
        private static IEnumerable<TModel> TransferLogicFrom<TModel, TOther>(this IEnumerable<TModel> source, IEnumerable<TOther> other, Func<TModel, int> modelIdSelector, Func<TOther, int> otherIdSelector, bool isMerge = false, bool isNew = false)
        {
            if (source is null) throw new ArgumentNullException($"{typeof(TModel).Name} source can't be null");
            if (other is null) throw new ArgumentNullException($"{typeof(TOther).Name} other can't be null");

            foreach (TModel item in source)
            {
                TOther otherItem = other.First(other => otherIdSelector(other) == modelIdSelector(item));

                yield return item.TransferLogicFrom(otherItem, isMerge, isNew);
            }
        }



        public static TModel MergeWith<TModel, TOther>(this TModel source, TOther other)
        {
            return source.TransferLogicFrom(other, true);
        }
        public static TModel MergeWith<TModel>(this TModel source, TModel other)
        {
            return source.TransferLogicFrom(other, true);
        }
        public static IEnumerable<TModel> MergeWith<TModel>(this IEnumerable<TModel> sources, IEnumerable<TModel> others, Func<TModel, int> modelIdSelector)
        {
            return sources.TransferLogicFrom(others, modelIdSelector, modelIdSelector, true);
        }
        public static IEnumerable<TModel> MergeWith<TModel, TOther>(this IEnumerable<TModel> sources, IEnumerable<TOther> others, Func<TModel, int> modelIdSelector, Func<TOther, int> otherIdSelector)
        {
            return sources.TransferLogicFrom(others, modelIdSelector, otherIdSelector, true);
        }



        public static TModel ConvertTo<TModel, TOther>(this TOther other, int id)
            where TModel : IModel
        {
            TModel model = Activator.CreateInstance<TModel>();
            model.Id = id;

            return model.TransferLogicFrom(other, false, true);
        }
        public static TModel ConvertTo<TModel, TOther>(this TOther other)
        {
            TModel model = Activator.CreateInstance<TModel>();

            return model.TransferLogicFrom(other, false, false);
        }
        public static TModel ConvertTo<TModel>(this TModel other)
        {
            return other.ConvertTo();
        }
        public static IEnumerable<TModel> ConvertTo<TModel, TOther>(this IEnumerable<TOther> others)
        {
            foreach (TOther otherItem in others)
                yield return otherItem.ConvertTo<TModel, TOther>();
        }
        public static IEnumerable<TModel> ConvertTo<TModel>(this IEnumerable<TModel> others)
        {
            return others.ConvertTo();
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
                TRelated related = baseRelatedSelector(relatedId).ConvertTo<TRelated, TBaseRelated>();
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
        public static IEnumerable<TModel> MergeManyToOne<TModel, TRelated>(
            this IEnumerable<TModel> source,
            Func<TModel, List<TRelated>> relatedEnumerableSelector,
            Func<int, List<TRelated>> relatedFetcher)
            where TModel : IModel
            where TRelated : IModel
        {
            foreach (TModel item in source)
            {
                List<TRelated> relateds = relatedEnumerableSelector(item);
                List<TRelated> relatedModels = relatedFetcher(item.Id);

                foreach (TRelated relatedModel in relatedModels)
                    relateds.Add(relatedModel);

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
                    relateds.Add(relatedFetcher(relatedId).ConvertTo<TRelated, TRelatedModel>());
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
