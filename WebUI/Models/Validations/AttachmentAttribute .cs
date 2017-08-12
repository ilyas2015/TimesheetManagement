using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace WebUI.Models.Validations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AttachmentAttribute: ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;

            if (file == null)
            {
                return new ValidationResult("Please upload a file!");
            }
            if (file.ContentLength > 10 * 1024 * 1024)
            {
                return new ValidationResult("This file is too big!");
            }
            string ext = Path.GetExtension(file.FileName);
            if (String.IsNullOrEmpty(ext) ||
               !ext.Equals(".docx", StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult("This file is not a word document(*.docx)!");
            }
            return ValidationResult.Success;
        }

        

    }
}