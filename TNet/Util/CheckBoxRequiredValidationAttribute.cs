using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TNet.Util {

    /// <summary>
    /// 验证Checkbox必须选中其中一个
    /// </summary>
    public class CheckBoxRequiredValidationAttribute:ValidationAttribute,IClientValidatable {
        public override string FormatErrorMessage(string name) {
            string message = string.Format("请勾选{0}",name);
            return message;
        }

        public override bool IsValid(object value) {
            return value != null ? true : false;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context) {
            var validationRule = new ModelClientValidationRule {
                ErrorMessage = FormatErrorMessage(metadata.DisplayName),
                ValidationType = "CheckBox",
            };

            yield return validationRule;
        }
    }
}