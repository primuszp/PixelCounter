using System.Globalization;

namespace PixelCounter
{
    internal static class Strings
    {
        static Strings()
        {
            bool hu = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "hu";

            FolderGroupTitle   = hu ? "Forrás"                     : "Source";
            FolderButton       = hu ? "Mappa"                      : "Folder";
            PaperGroupTitle    = hu ? "Papír mérete"               : "Paper size";
            WidthLabel         = hu ? "Szélesség:"                 : "Width:";
            ProcessGroupTitle  = hu ? "Feldolgozás"                : "Processing";
            ConvertLabel       = hu ? "Konvertálás:"               : "Convert:";
            ConvertCheck       = hu ? "Fekete/Fehér"               : "Black/White";
            FillHolesCheck     = hu ? "Lyukkitöltés"               : "Fill holes";
            ComponentsCheck    = hu ? "Komponensek"                : "Components";
            MinSizeLabel       = hu ? "Min. méret (px):"           : "Min. size (px):";
            AutoDpiCheck       = hu ? "Automatikus (DPI metaadat)" : "Auto (image DPI)";
            ExitButton         = hu ? "Kilépés"                    : "Exit";
            Ready              = hu ? "Feldolgozva:"               : "Processed:";
            ProcessingDone     = hu ? "Feldolgozás befejezve"      : "Processing complete";
            DpiDetected        = hu ? "{0:0} × {1:0} DPI (metaadat)" : "{0:0} × {1:0} DPI (from metadata)";
            DpiNotFound        = hu ? "DPI nem található – papírméret használva" : "DPI not found – using paper size";
            ReportHeaderNormal = hu
                ? "Fájlnév\tFekete Px (db)\tFehér Px (db)\tEgyéb Px (db)\tArány (%)\tTerület (cm²)"
                : "Filename\tBlack Px\tWhite Px\tOther Px\tRatio (%)\tArea (cm²)";
            ReportHeaderComponents = hu
                ? "Fájlnév\tKomponens #\tFekete Px (db)\tTerület (cm²)"
                : "Filename\tComponent #\tBlack Px\tArea (cm²)";
        }

        public static readonly string FolderGroupTitle;
        public static readonly string FolderButton;
        public static readonly string PaperGroupTitle;
        public static readonly string WidthLabel;
        public static readonly string ProcessGroupTitle;
        public static readonly string ConvertLabel;
        public static readonly string ConvertCheck;
        public static readonly string FillHolesCheck;
        public static readonly string ComponentsCheck;
        public static readonly string MinSizeLabel;
        public static readonly string AutoDpiCheck;
        public static readonly string ExitButton;
        public static readonly string Ready;
        public static readonly string ProcessingDone;
        public static readonly string DpiDetected;
        public static readonly string DpiNotFound;
        public static readonly string ReportHeaderNormal;
        public static readonly string ReportHeaderComponents;
    }
}
