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
            where TModelDTO : class, new()
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
            where TModelDTO : class, new()
            where TModel : IModelDAL
        {
            List<TModelDTO> list = new();

            foreach (TModel model in models)
                list.Add(model.ToDTO<TModelDTO, TModel>());

            return list;
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
                    object value = prop.GetValue(otherItem);
                    if (value is not null) prop.SetValue(item, value);
                }

                yield return item;
            }
        }
    }
}









/*

        public static TModelDTO ToDTO<TModelDTO, TModel>(this TModel model)
            where TModelDTO : class, new()
            where TModel : IModelDAL
        {
            TModelDTO dto = model.ToDTO<TModelDTO, TModel>();
            PropertyInfo[] props = typeof(TModelDTO).GetProperties();
            object? value;

            if (typeof(DataDTO).IsAssignableFrom(typeof(TModelDTO)))
            {
                Data dataDTO = dataService.GetById(model.Id);
                foreach (PropertyInfo prop in props)
                {
                    PropertyInfo dataProp = typeof(Data).GetProperty(prop.Name);
                    if (dataProp is not null && dataProp.PropertyType == prop.PropertyType)
                    {
                        value = dataProp.GetValue(dataDTO);
                        if (value is not null) prop.SetValue(dto, value);
                    }
                    else if (prop.GetValue(dto) is IEnumerable<CategoryDTO>)
                    {
                        IEnumerable<SoftwareCategory> dalList = programCategoryService.GetById<Software>(model.Id);
                        List<CategoryDTO> categories = new();

                        foreach (SoftwareCategory programCat in dalList)
                            categories.Add(categoryService.GetById(programCat.CategoryId).ToDTO<CategoryDTO, Category>());

                        prop.SetValue(dto, categories);
                    }
                }
            }

            return dto;
        }


 */