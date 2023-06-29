namespace apollo.Domain.Entities.Core
{
    //fileName.Contains('.') ? (int)Enum.Parse<DocType>(fileName.Split('.')[1], true) : (int)Enum.Parse<DocType>(fileTitle.Split('.')[1], true)
    public enum DocType
    {

        Pdf = 100,
        Txt = 120,

        Rtf = 121,
        Csv = 122,

        // Document
        Doc = 200,
        Docx = 201,
        Dot = 203,
        Dotx = 204,

        Xls = 220,
        Xlsx = 221,

        Ppt = 240,
        Pptx = 241,

        Odt = 260,

        Key = 281,

        // Image
        Png = 300,

        Jpg = 320,
        Jpeg = 321,

        Gif = 340,

        Bmp = 360,
        Tif = 361,
        Tiff = 362,
        Svg = 363,

        Psd = 380,

        // Audio
        Mp3 = 400,
        M4A = 402,
        Ogg = 403,
        Wav = 404,

        // Video
        Mp4 = 501,
        M4V = 502,
        Mpg = 510,
        Mpeg = 511,
        Avi = 520,
        Mov = 521,
        Wmv = 530,
        Wma = 531,
        Flv = 540,
        Ogv = 541
    }

    public enum DocSourceType
    {
        Blob,
        External,
        Filestack
    }
}
