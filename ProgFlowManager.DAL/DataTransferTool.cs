using ProgFlowManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL
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
    }
}
