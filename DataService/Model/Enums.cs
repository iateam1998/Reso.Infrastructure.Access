using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataService.Model.ViewModel
{
    public static partial class Enums
    {
        public enum ApplicationOriginEnum
        {
            [Display(
                Name = "I"
                , Description = "Project developed by a software development company."
            )]
            I = 1,
            [Display(
                Name = "U"
                , Description = "Projects developed by university staff and students for the internal usage at the university."
            )]
            U = 2,
            [Display(
                Name = "S2B"
                , Description = "Project developed by students for external organizations")]
            S2B = 3
        }

        public enum ApplicationTypeEnum
        {
            [Display(
                Name = "N"
                , Description = "Application was developed from scratch."
            )]
            N = 1,
            [Display(
                Name = "C"
                , Description = "Application was based on existing solution and was tailored for the customer."
            )]
            C = 2,
            [Display(
                Name = "E"
                , Description = "Major enhancement, i.e., strongly simplified version was available (e.g. a prototype)")]
            E = 3
        }

        public enum ApplicationStatusEnum
        {
            [Display(
                Name = "To Do"
            )]
            ToDo = 1,
            [Display(
                Name = "Doing"
            )]
            Doing = 2,
            [Display(
                Name = "Done"
            )]
            Done = 3,
            [Display(
                Name = "Pending"
            )]
            Pending = 4,
            [Display(
                Name = "Cancel"
            )]
            Cancel = 5
        }

        public enum ApplicationPriorityEnum
        {
            [Display(
                Name = "Low"
            )]
            Low = 1,
            [Display(
                Name = "Normal"
            )]
            Normal = 2,
            [Display(
                Name = "High"
            )]
            High = 3,
            [Display(
                Name = "Important"
            )]
            Important = 4
        }

        public enum ApplicationStageEnum
        {
            [Display(
                Name = "Develop"
            )]
            Develop = 1,
            [Display(
                Name = "Staging"
            )]
            Staging = 2,
            [Display(
                Name = "Product"
            )]
            Product = 3,
        }

        public enum ApplicationCategoryEnum
        {
            [Display(
                Name = "Web Application"
            )]
            Web = 1,
            [Display(
                Name = "Mobile Application"
            )]
            Mobile = 2,
            [Display(
                Name = "POS Application"
            )]
            POS = 3,
        }

        public enum UseCasePriorityEnum
        {
            [Display(
                Name = "Low"
            )]
            Low = 1,
            [Display(
                Name = "Normal"
            )]
            Normal = 2,
            [Display(
                Name = "High"
            )]
            High = 3,
            [Display(
                Name = "Important"
            )]
            Important = 4
        }

        public enum UseCaseStatusEnum
        {
            [Display(
                Name = "To Do"
            )]
            ToDo = 1,
            [Display(
                Name = "Doing"
            )]
            Doing = 2,
            [Display(
                Name = "Done"
            )]
            Done = 3,
            [Display(
                Name = "Pending"
            )]
            Pending = 4,
            [Display(
                Name = "Cancel"
            )]
            Cancel = 5
        }

        public enum CharacteristicDefinition
        {
            [Display(
                                Name = "Environmental Complexity Factor",
                Description = "Để tính toán ECF, mỗi yếu tố môi trường được gán một giá trị dựa trên cấp độ kinh nghiệm của nhóm."
            )]
            ECF = 1,
            [Display(
                                Name = "Technical Complexity Factor",
                Description = "Để tính toán TCF, mỗi yếu tố kỹ thuật được gán một giá trị dựa trên mức độ thiết yếu của khía cạnh kỹ thuật đối với hệ thống đang được phát triển."
            )]
            TCF = 2,
            [Display(
                                Name = "Unadjusted Actor Weight",
                Description = "UAW được tính toán dựa trên số lượng và độ phức tạp của các tác nhân cho hệ thống."
            )]
            UAW = 3,
            [Display(
                                Name = "Environmental Complexity Factor",
                Description = "UUCW được tính toán dựa trên số lượng và độ phức tạp của các trường hợp sử dụng cho hệ thống."
            )]
            UUCW = 4
        }
        public static IDictionary<int, string> Get<Type>()
        {
            var type = typeof(Type);
            var enumType = typeof(Enum);
            var enums = type.GetFields().Where(f => f.FieldType.IsSubclassOf(enumType));
            var dict = new Dictionary<int, string>();
            foreach (var e in enums)
            {
                dict[(int)e.GetRawConstantValue()] = e.FieldType.DisplayName(e.Name);
            }
            return dict;
        }

        public static string DisplayName(this Enum enumVal)
        {
            var enumType = enumVal.GetType();
            var name = Enum.GetName(enumType, enumVal);
            var displayNameAttr = enumType.GetField(name).GetCustomAttributes(false)
                .OfType<DisplayAttribute>().SingleOrDefault();
            if (displayNameAttr != null)
                return displayNameAttr.Name;
            return null;
        }

        public static String DisplayName(this Type enumType, string enumName)
        {
            var displayNameAttr = enumType.GetField(enumName).GetCustomAttributes(false)
                .OfType<DisplayAttribute>().SingleOrDefault();
            if (displayNameAttr != null)
            {
                if (displayNameAttr.Description != null)
                {
                    return displayNameAttr.Name + " - Description: " + displayNameAttr.Description;
                }
                else
                {
                    return displayNameAttr.Name;
                }
            }
            return null;
        }
    }
}
