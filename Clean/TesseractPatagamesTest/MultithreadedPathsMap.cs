using System.Collections.Generic;

namespace TesseractPatagamesTest
{
    class MultithreadedPathsMap
    {
        public static readonly Dictionary<DocumentType, string> DocumentTypeAnnotations = new Dictionary<DocumentType, string>()
        {
            {DocumentType.Account, "СЧЕТ"},
            {DocumentType.Act, "АКТ"},
            {DocumentType.Contract, "ДОГОВОР"},
            {DocumentType.Other, "ПРОЧЕЕ"},
        };

        public static readonly List<PathThreads> Paths = new List<PathThreads>()
        {
            new PathThreads()
            {
                Path = @"C:\Users\d.kolesov\Documents\dbrain\txts\Aкты_2"      ,
                MaxThreadCnt = 3,
                DocumentType = DocumentType.Act,
                Conditions = new List<List<string>>()
                {
                    new List<string>(){"акт"},
                    new List<string>(){"исполнитель"},
                    new List<string>(){"заказчик", "клиент"}
                }

            },

            new PathThreads()
            {
                Path = @"C:\Users\d.kolesov\Documents\dbrain\txts\mts_2\"       ,
                MaxThreadCnt = 1,
                DocumentType = DocumentType.Contract,
                Conditions = new List<List<string>>()
                {
                    new List<string>(){"договор" , "мтс","абонент","тариф"}
                }
            },

            new PathThreads()
            {
                Path = @"C:\Users\d.kolesov\Documents\dbrain\txts\Акты\"        ,
                MaxThreadCnt = 1,
                DocumentType = DocumentType.Act,
                Conditions = new List<List<string>>()
                {
                    new List<string>(){"акт"},
                    new List<string>(){"исполнитель"},
                    new List<string>(){"заказчик", "клиент"}
                }
            },

            new PathThreads()
            {
                Path = @"C:\Users\d.kolesov\Documents\dbrain\txts\МТС_ТЕСТ\"    ,
                MaxThreadCnt = 1,
                DocumentType = DocumentType.Contract,
                Conditions = new List<List<string>>()
                {
                    new List<string>(){"договор" , "мтс","абонент","тариф"}
                }
            },

            new PathThreads()
            {
                Path = @"C:\Users\d.kolesov\Documents\dbrain\txts\Прочее\"      ,
                MaxThreadCnt = 1,
                DocumentType = DocumentType.Other,
                Conditions = new List<List<string>>()
                {
                    new List<string>(){"товарная", "накладная"},
                    new List<string>(){"счет", "фактура"},
                    new List<string>(){"акт", "прием", "передач"},
                    new List<string>(){"сдал", "принял"}
                }
            },

            new PathThreads()
            {
                Path = @"C:\Users\d.kolesov\Documents\dbrain\txts\Счета\"       ,
                MaxThreadCnt = 1,
                DocumentType = DocumentType.Account,
                Conditions = new List<List<string>>()
                {
                    new List<string>(){"счет"},
                    new List<string>(){"покупатель", "плательщик"},
                    new List<string>(){"поставщик", "получатель"}
                }
            },

            new PathThreads()
            {
                Path = @"C:\Users\d.kolesov\Documents\dbrain\txts\Счета_2\"      ,
                MaxThreadCnt = 1,
                DocumentType = DocumentType.Account,
                Conditions = new List<List<string>>()
                {
                    new List<string>(){"счет"},
                    new List<string>(){"покупатель", "плательщик"},
                    new List<string>(){"поставщик", "получатель"}
                }
            },
        };

        public static readonly List<PathThreads> ConditionsMap = new List<PathThreads>()
        {
            new PathThreads()
            {
                Path = DocumentTypeAnnotations[DocumentType.Account],
                DocumentType = DocumentType.Account,
                Conditions = new List<List<string>>()
                {
                    new List<string>(){"счет"},
                    new List<string>(){"покупатель", "плательщик"},
                    new List<string>(){"поставщик", "получатель"}
                }
            },

            new PathThreads()
            {
                Path = DocumentTypeAnnotations[DocumentType.Other],
                DocumentType = DocumentType.Other,
                Conditions = new List<List<string>>()
                {
                    new List<string>(){"товарная", "накладная"},
                    new List<string>(){"счет", "фактура"},
                    new List<string>(){"акт", "прием", "передач"},
                    new List<string>(){"сдал", "принял"}
                }
            },

            new PathThreads()
            {
                Path = DocumentTypeAnnotations[DocumentType.Contract],
                DocumentType = DocumentType.Contract,
                Conditions = new List<List<string>>()
                {
                    new List<string>(){"договор" , "мтс","абонент","тариф"}
                }
            },

            new PathThreads()
            {
                Path = DocumentTypeAnnotations[DocumentType.Act],
                DocumentType = DocumentType.Act,
                Conditions = new List<List<string>>()
                {
                    new List<string>(){"акт"},
                    new List<string>(){"исполнитель"},
                    new List<string>(){"заказчик", "клиент"}
                }
            },

        };
    }
}
