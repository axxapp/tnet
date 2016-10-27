$.validator.addMethod("chkrequired",
           function (value, element, params) {
               return value != null;
           });

$.validator.unobtrusive.adapters.add("chkrequired", [], function (options) {
    options.rules["chkrequired"] = {
    };
    options.messages["chkrequired"] = options.message;
});

