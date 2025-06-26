using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace TravelbilityApp.WebAPI.ModelBinders
{
    public class CommaSeparatedModelBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Вземаме стойността от query string, form или route
            var valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            // Ако няма стойност, връщаме null (или успех с null)
            if (valueResult == ValueProviderResult.None ||
                string.IsNullOrEmpty(valueResult.FirstValue))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            // Стойността като низ, може да е "1,2,3"
            var raw = valueResult.FirstValue!;

            // Сплитваме по запетая
            var parts = raw
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .ToArray();

            try
            {
                // Конвертираме всяка част към T
                var converter = TypeDescriptor.GetConverter(typeof(T));
                var typed = parts
                    .Select(p => (T)converter.ConvertFromString(p))
                    .ToArray();

                // Успешен резултат
                bindingContext.Result = ModelBindingResult.Success(typed);
            }
            catch (Exception ex) when (ex is FormatException || ex is NotSupportedException)
            {
                // Грешка при конверсия → добавяме ModelState error
                bindingContext.ModelState.AddModelError(
                    bindingContext.ModelName,
                    $"Не може да конвертира '{raw}' към {typeof(T).Name}: {ex.Message}");
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }
}
