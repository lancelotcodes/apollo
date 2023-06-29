using apollo.Application.Agents.Queries.DTOs;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using D = DocumentFormat.OpenXml.Drawing;

namespace apollo.Presentation.Utils
{
    public static class OfferSlideHelper
    {
        public static IConfiguration Configuration { get; set; }
        public static void UpdateClientNameInFirstSlide(PresentationPart presentationPart, string firstName, string lastName)
        {
            var items = presentationPart.Presentation.SlideIdList;

            if (items.Count() > 0)
            {
                SlideId item = (SlideId)items.FirstOrDefault();
                var part = presentationPart.GetPartById(item.RelationshipId);
                var slidePart = (part as SlidePart);
                var textBody = slidePart.Slide.Descendants<TextBody>();
                if (textBody != null && textBody.Any())
                {
                    var paragraph = textBody.First().Descendants<D.Paragraph>().FirstOrDefault(x => x.InnerText == "Insert Client Name");

                    if (paragraph.Descendants<D.Run>().Count() > 1)
                    {
                        // Find the run that contains the text you want to update
                        var insertRun = paragraph.Descendants<D.Run>().First();

                        // Update the text content of the run
                        var insertText = insertRun.Descendants<D.Text>().First();
                        insertText.Text = $"{firstName} ";

                        // Find the run that contains the text you want to update
                        var run = paragraph.Descendants<D.Run>().ElementAt(1);

                        // Update the text content of the run
                        var textElement = run.Descendants<D.Text>().First();
                        textElement.Text = lastName;
                        slidePart.Slide.Save();
                    }
                }
            }
        }

        public static void InsertAgentDetails(PresentationPart presentationPart, ContactDTO contactDTO)
        {
            var items = presentationPart.Presentation.SlideIdList;

            if (items.Count() > 0)
            {
                SlideId item = (SlideId)items.LastOrDefault();
                var part = presentationPart.GetPartById(item.RelationshipId);
                var slidePart = (part as SlidePart);
                var textBodies = slidePart.Slide.Descendants<TextBody>();
                if (textBodies != null && textBodies.Any())
                {
                    var textBody = textBodies.FirstOrDefault();
                    textBody.RemoveAllChildren<D.Paragraph>();
                    textBody.BodyProperties = new D.BodyProperties { UpRight = true };
                    AddParagraph(textBody, contactDTO.FirstName + " " + contactDTO.LastName, false, System.Drawing.Color.White, 1800);
                    AddParagraph(textBody, contactDTO.Email, false, System.Drawing.Color.White, 1800);
                    AddParagraph(textBody, contactDTO.PhoneNumber, false, System.Drawing.Color.White, 1800);
                }
            }
        }

        public static SlidePart GetCitySlide(PresentationDocument doc, string cityName, string locationType)
        {
            SlidePart newSlidePart = PowerPointUtils.GetNewSlideWIthLayout(doc.PresentationPart, "BGC");
            if (newSlidePart != null)
            {
                var shapes = newSlidePart.Slide.Descendants<Shape>();

                var shape1 = shapes.FirstOrDefault(x => x.InnerText == "Click to edit Master title style");
                if (shape1 != null)
                {
                    var titleTextBody = shape1.Descendants<TextBody>();
                    var titleParaGraph = titleTextBody.First().Descendants<D.Paragraph>().FirstOrDefault();
                    var titleRun = titleParaGraph.Descendants<D.Run>().First();

                    // Update the text content of the run
                    var titleText = titleRun.Descendants<D.Text>().First();
                    titleText.Text = cityName + " " + locationType;
                }
            }
            return newSlidePart;
        }

        public static SlidePart GetUnitSlide(PresentationDocument doc, string cityName, string buildingName, string buildingImageHandle, string floorImageHandle, List<Dictionary<string, string>> data, string address, string mapUrl)
        {
            //SlidePart newSlidePart = PowerPointUtils.GetNewSlideWIthLayout(doc.PresentationPart, "6_Building Option - Portrait");
            SlidePart newSlidePart = PowerPointUtils.GetNewSlideWIthLayout(doc.PresentationPart, "4_Building Option - Portrait");
            if (newSlidePart != null)
            {
                var shapes = newSlidePart.Slide.Descendants<Shape>();

                var shape1 = shapes.FirstOrDefault(x => x.InnerText == "Click to edit Master title style");
                if (shape1 != null)
                {
                    var titleTextBody = shape1.Descendants<TextBody>();
                    var titleParaGraph = titleTextBody.First().Descendants<D.Paragraph>().FirstOrDefault();
                    var titleRun = titleParaGraph.Descendants<D.Run>().First();

                    // Update the text content of the run
                    var titleText = titleRun.Descendants<D.Text>().First();
                    titleText.Text = cityName + " | " + buildingName;
                }

                var shape2 = shapes.FirstOrDefault(x => x.InnerText == "Building Photo");
                if (shape2 != null)
                {
                    shape2.RemoveAllChildren<TextBody>();
                    if (!string.IsNullOrWhiteSpace(buildingImageHandle))
                    {
                        ReplaceShapeWithImageFromURL(newSlidePart, shape2, buildingImageHandle);
                    }
                }

                var shape3 = shapes.FirstOrDefault(x => x.InnerText == "Floor plan");
                if (shape3 != null)
                {
                    shape3.RemoveAllChildren<TextBody>();
                    if (!string.IsNullOrWhiteSpace(floorImageHandle))
                    {
                        ReplaceShapeWithImageFromURL(newSlidePart, shape3, floorImageHandle);
                    }
                }

                var shape4 = shapes.FirstOrDefault(x => x.InnerText == "Building and Lease Information");
                if (shape4 != null)
                {
                    shape4.RemoveAllChildren<TextBody>();
                    ReplaceShapeWithTable(newSlidePart, shape4, data, address);
                    newSlidePart.Slide.Save();
                    doc.Save();
                }

                InsertPropertyURLShape(newSlidePart, mapUrl);
            }
            return newSlidePart;
        }

        private static void InsertPropertyURLShape(SlidePart newSlidePart, string mapUrl)
        {

            D.Offset offset = new D.Offset { X = 303194, Y = 6179316 };
            D.Extents extents = new D.Extents { Cx = 3797750, Cy = 292797 };

            UInt32Value linkId = 10002U;
            string name = "link" + linkId.ToString();

            var shape = new Shape()
            {
                NonVisualShapeProperties = new NonVisualShapeProperties()
                {
                    NonVisualDrawingProperties = new NonVisualDrawingProperties()
                    {
                        Id = linkId,
                        Name = name
                    },
                    NonVisualShapeDrawingProperties = new NonVisualShapeDrawingProperties()
                    {
                        ShapeLocks = new D.ShapeLocks() { NoGrouping = true }
                    },
                    ApplicationNonVisualDrawingProperties = new ApplicationNonVisualDrawingProperties()
                    {
                        PlaceholderShape = new PlaceholderShape() { Type = PlaceholderValues.Object }
                    }
                },
                ShapeProperties = new ShapeProperties()
                {
                    Transform2D = new D.Transform2D
                    {
                        Extents = extents,
                        Offset = offset
                    }
                },
            };


            D.Paragraph paragraph = new D.Paragraph();
            paragraph.ParagraphProperties = new D.ParagraphProperties()
            {
                Alignment = D.TextAlignmentTypeValues.Center
            };


            D.Run run = new D.Run();
            D.RunProperties runProperties = new D.RunProperties
            {
                FontSize = 12 * 100, //
            };

            var textSolidFill = new D.SolidFill();
            var rgbColorModelHex = new D.RgbColorModelHex() { Val = "FFFFFF" };

            textSolidFill.Append(rgbColorModelHex);
            runProperties.Append(textSolidFill);

            run.Append(runProperties);
            D.Text textObj = new D.Text { Text = "Click here to view location" };
            run.Append(textObj);
            paragraph.Append(run);

            shape.TextBody = new TextBody(new D.BodyProperties(), paragraph);

            // Create a new SolidFill with the specified color
            D.SolidFill solidFill = new D.SolidFill(
                new D.RgbColorModelHex { Val = "1A3865" }
            );

            // Get or create the ShapeProperties for the shape
            ShapeProperties shapeProperties = shape.GetFirstChild<ShapeProperties>();
            if (shapeProperties == null)
            {
                shapeProperties = new ShapeProperties();
                shape.Append(shapeProperties);
            }

            // Apply the new solid fill
            shapeProperties.Append(solidFill);

            // Add the shape to the slide part
            newSlidePart.Slide.CommonSlideData.ShapeTree.AppendChild(shape);

            var hyperlinkRelationship = newSlidePart.AddHyperlinkRelationship(new Uri(mapUrl), true);
            var hyperlinkId = hyperlinkRelationship.Id;

            var hyperlinkClick = new D.HyperlinkOnClick()
            {
                Id = hyperlinkId,
            };

            shape.NonVisualShapeProperties.NonVisualDrawingProperties.HyperlinkOnClick = hyperlinkClick;

            // Save the changes
            newSlidePart.Slide.Save();
        }

        public static SlidePart GetOptionsSlideByCity(PresentationDocument doc, string cityName, string locationType, IEnumerable<string> units, double latitude, double longitude, double[][] coordinates)
        {
            SlidePart newSlidePart = PowerPointUtils.GetNewSlideWIthLayout(doc.PresentationPart, "Transpo Map");

            if (newSlidePart != null)
            {
                var shapes = newSlidePart.Slide.Descendants<Shape>();

                var shape1 = shapes.FirstOrDefault(x => x.InnerText == "Click to edit Master title style");
                if (shape1 != null)
                {
                    var titleTextBody = shape1.Descendants<TextBody>();
                    var titleParaGraph = titleTextBody.First().Descendants<D.Paragraph>().FirstOrDefault();
                    var titleRun = titleParaGraph.Descendants<D.Run>().First();

                    // Update the text content of the run
                    var titleText = titleRun.Descendants<D.Text>().First();
                    titleText.Text = cityName + " " + locationType;
                }

                var shape2 = shapes.FirstOrDefault(x => x.InnerText.Contains("Map"));
                if (shape2 != null)
                {
                    shape2.RemoveAllChildren<TextBody>();

                    // Need to modify this to read token from App settings

                    var arrayString = string.Empty;
                    if (coordinates != null && coordinates.Any())
                    {
                        foreach (var item in coordinates)
                        {
                            if (!string.IsNullOrWhiteSpace(arrayString))
                            {
                                arrayString += ",";
                            }
                            arrayString += $"[{item[0]},{item[1]}]";
                        }
                    }

                    string geoJsonString = $"{{\"type\":\"FeatureCollection\",\"features\":[{{\"type\":\"Feature\",\"geometry\":{{\"type\":\"MultiPoint\",\"coordinates\":[{arrayString}]}},\"properties\":{{\"title\":\"Building Location\",\"marker-symbol\":\"building\",\"marker-size\":\"large\",\"marker-color\":\"#1027ef\"}}}}]}}";

                    string encodedGeoJsonString = HttpUtility.UrlEncode(geoJsonString);

                    string mapImageUrl = $"https://api.mapbox.com/styles/v1/mapbox/streets-v12/static/geojson({encodedGeoJsonString})/{longitude},{latitude},14,20/1200x1000?access_token={Configuration["MapBoxKey"]}";

                    ReplaceShapeWithImageFromURL(newSlidePart, shape2, mapImageUrl);

                    newSlidePart.Slide.Save();
                    doc.Save();
                }

                var shape3 = shapes.ElementAt(0);
                if (shape3 != null)
                {
                    TextBody docBody = shape3.TextBody;
                    docBody.BodyProperties = new D.BodyProperties { UpRight = true };
                    docBody.RemoveAllChildren<D.Paragraph>();
                    foreach (var item in units)
                    {
                        AddParagraph(docBody, item, true, System.Drawing.Color.White, 1200);
                    }
                }
            }
            return newSlidePart;
        }

        private static void ReplaceShapeWithImageFromURL(SlidePart newSlidePart, Shape shape2, string mapImageUrl)
        {
            bool isImageDowloaded = false;
            ImagePart imagePart = newSlidePart.AddImagePart(ImagePartType.Png);
            try
            {
                Stream imageStream = UrlImageUtils.GetStreamFromUrl(mapImageUrl);
                imagePart.FeedData(imageStream);
                isImageDowloaded = true;
            }
            catch (Exception)
            {
            }

            if (isImageDowloaded)
            {
                // Get the position and size of the shape
                Picture picture = new Picture();

                UInt32Value picId = 10001U;
                string name = string.Empty;
                name = "image" + picId.ToString();

                NonVisualPictureProperties nonVisualPictureProperties = new NonVisualPictureProperties()
                {
                    NonVisualDrawingProperties = new NonVisualDrawingProperties() { Name = name, Id = picId, Title = name },
                    NonVisualPictureDrawingProperties = new NonVisualPictureDrawingProperties() { PictureLocks = new D.PictureLocks() { NoChangeAspect = true } },
                    ApplicationNonVisualDrawingProperties = new ApplicationNonVisualDrawingProperties() { UserDrawn = true }
                };

                BlipFill blipFill = new BlipFill() { Blip = new D.Blip() { Embed = newSlidePart.GetIdOfPart(imagePart) } };
                D.Stretch stretch = new D.Stretch() { FillRectangle = new D.FillRectangle() };
                blipFill.Append(stretch);

                ShapeProperties shapeProperties = (ShapeProperties)shape2.ShapeProperties.Clone();
                picture.Append(nonVisualPictureProperties);
                picture.Append(blipFill);
                picture.Append(shapeProperties);

                {
                    shape2.Parent.ReplaceChild(picture, shape2);
                }
            }
        }

        private static void ReplaceShapeWithTable(SlidePart newSlidePart, Shape shape2, List<Dictionary<string, string>> data, string address)
        {
            ShapeTree shapeTree = newSlidePart.Slide.Descendants<ShapeTree>().FirstOrDefault();

            // Create a table
            int numCols = 4;
            var graphicFrame = CreateTable(shape2, numCols, data, address);
            shapeTree.ReplaceChild(graphicFrame, shape2);
        }

        public static void AddParagraph(TextBody docBody, string NewText, bool isBulleted, System.Drawing.Color color, int fontSize)
        {
            string _bulletParagraphOuterXML = "<a:pPr xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\"><a:buFont typeface=\"Arial\" panose=\"020B0604020202020204\" pitchFamily=\"34\" charset=\"0\" /><a:buAutoNum type=\"arabicPeriod\"/></a:pPr>";
            D.Paragraph p = new D.Paragraph();
            p.ParagraphProperties = isBulleted ? new D.ParagraphProperties(_bulletParagraphOuterXML) : new D.ParagraphProperties();

            D.Run run = new D.Run();
            D.RunProperties runProp = new D.RunProperties() { Language = "en-US", FontSize = fontSize };

            var hexColor = color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");//convert color to hex
            var solidFill = new D.SolidFill();
            var rgbColorModelHex = new D.RgbColorModelHex() { Val = hexColor };

            solidFill.Append(rgbColorModelHex);
            runProp.Append(solidFill);
            run.AppendChild(runProp);

            D.Text newText = new D.Text(NewText);
            run.AppendChild(newText);
            p.Append(run);
            docBody.Append(p);
        }

        public static D.TableCell CreateTextCell(string text, string backgroundColor, int fontSize, string color, int colSpan, bool isBold)
        {

            D.TableCellProperties tableCellProperties = new D.TableCellProperties();

            tableCellProperties.BottomMargin = 91833;
            tableCellProperties.TopMargin = 91833;
            tableCellProperties.RightMargin = 42202;
            tableCellProperties.LeftMargin = 42202;

            D.BottomBorderLineProperties bottomBorderLineProperties1 = new D.BottomBorderLineProperties() { Width = 0, CapType = D.LineCapValues.Round, CompoundLineType = D.CompoundLineValues.Single, Alignment = D.PenAlignmentValues.Center };
            D.NoFill noFill = new D.NoFill();
            bottomBorderLineProperties1.Append(noFill);

            D.TopBorderLineProperties topBorderLineProperties = new D.TopBorderLineProperties() { Width = 0, CapType = D.LineCapValues.Round, CompoundLineType = D.CompoundLineValues.Single, Alignment = D.PenAlignmentValues.Center };
            D.NoFill noFill1 = new D.NoFill();
            topBorderLineProperties.Append(noFill1);

            D.LeftBorderLineProperties leftBorderLineProperties = new D.LeftBorderLineProperties() { Width = 0, CapType = D.LineCapValues.Round, CompoundLineType = D.CompoundLineValues.Single, Alignment = D.PenAlignmentValues.Center };
            D.NoFill leftNoFill = new D.NoFill();
            leftBorderLineProperties.Append(leftNoFill);

            D.RightBorderLineProperties rightBorderLineProperties = new D.RightBorderLineProperties() { Width = 0, CapType = D.LineCapValues.Round, CompoundLineType = D.CompoundLineValues.Single, Alignment = D.PenAlignmentValues.Center };
            D.NoFill rightFill = new D.NoFill();
            rightBorderLineProperties.Append(rightFill);

            tableCellProperties.Append(leftBorderLineProperties);
            tableCellProperties.Append(rightBorderLineProperties);
            tableCellProperties.Append(topBorderLineProperties);
            tableCellProperties.Append(bottomBorderLineProperties1);

            var backgroundColorSolidFill = new D.SolidFill();
            var backgroundColorRgbHex = new D.RgbColorModelHex() { Val = backgroundColor };

            backgroundColorSolidFill.Append(backgroundColorRgbHex);
            tableCellProperties.Append(backgroundColorSolidFill);

            D.Paragraph paragraph = new D.Paragraph();
            D.Run run = new D.Run();
            D.RunProperties runProperties = new D.RunProperties
            {
                FontSize = fontSize * 100, // Font size in hundredths of a point
                Bold = isBold
            };

            var solidFill = new D.SolidFill();
            var rgbColorModelHex = new D.RgbColorModelHex() { Val = color };

            solidFill.Append(rgbColorModelHex);
            runProperties.Append(solidFill);

            run.Append(runProperties);
            D.Text textObj = new D.Text { Text = text };
            run.Append(textObj);
            paragraph.Append(run);

            D.TableCell tableCell = new D.TableCell(
                                    new D.TextBody(new D.BodyProperties(),
                                    paragraph), tableCellProperties);

            tableCell.GridSpan = colSpan;

            return tableCell;
        }

        private static GraphicFrame CreateTable(Shape shape2, int numCols, List<Dictionary<string, string>> data, string address)
        {
            D.Offset offset = new D.Offset { X = 4226340, Y = 1157514 };
            D.Extents extents = new D.Extents { Cx = 7560860, Cy = 5262532 };

            D.Transform2D transform = shape2.Descendants<D.Transform2D>().FirstOrDefault();

            if (transform != null)
            {
                extents = (D.Extents)transform.Extents.Clone();
                offset = (D.Offset)transform.Offset.Clone();
            }

            // Create a GraphicFrame to hold the table
            GraphicFrame graphicFrame = new GraphicFrame
            {
                NonVisualGraphicFrameProperties = new NonVisualGraphicFrameProperties
                {
                    NonVisualDrawingProperties = new NonVisualDrawingProperties { Id = 4U, Name = "Table" },
                    NonVisualGraphicFrameDrawingProperties = new NonVisualGraphicFrameDrawingProperties(),
                    ApplicationNonVisualDrawingProperties = new ApplicationNonVisualDrawingProperties()
                },
                Transform = new Transform
                {
                    Offset = offset,
                    Extents = extents
                }
            };

            // Set the GraphicFrame's GraphicData with the Table
            D.Graphic graphic = new D.Graphic();
            graphicFrame.Graphic = graphic;
            D.GraphicData graphicData = new D.GraphicData { Uri = "http://schemas.openxmlformats.org/drawingml/2006/table" };
            graphic.GraphicData = graphicData;

            // Create the Table element
            D.Table table = new D.Table();
            graphicData.Append(table);

            // Table style
            D.TableProperties tableProperties = new D.TableProperties();

            table.Append(tableProperties);
            // Create the TableGrid element with GridColumn elements
            D.TableGrid tableGrid = new D.TableGrid();
            table.Append(tableGrid);

            for (int col = 0; col < numCols; col++)
            {
                D.GridColumn gridColumn = new D.GridColumn { Width = extents.Cx / numCols };
                tableGrid.Append(gridColumn);
            }

            AddAddressHeaderRow(table, address);
            AddBuildingHeaderRow(table);

            string bgColor = "ECEFF6";

            foreach (var item in data)
            {
                AddOwnerRow(table, item, bgColor, 8, "000000", false);

                if (bgColor == "ECEFF6")
                {
                    bgColor = "D8DDEC";
                }
                else
                {
                    bgColor = "ECEFF6";
                }
            }

            return graphicFrame;
        }

        private static void AddAddressHeaderRow(D.Table table, string address)
        {
            D.TableRow tableRow = new D.TableRow();
            tableRow.Height = 220013;
            tableRow.Append(CreateTextCell(address, "203864", 14, "FFFFFF", 4, true));
            tableRow.Append(CreateTextCell("", "203864", 14, "FFFFFF", 1, true));
            tableRow.Append(CreateTextCell("", "203864", 14, "FFFFFF", 1, true));
            tableRow.Append(CreateTextCell("", "203864", 14, "FFFFFF", 1, true));
            table.Append(tableRow);
        }

        private static void AddBuildingHeaderRow(D.Table table)
        {
            D.TableRow tableRow = new D.TableRow();
            tableRow.Height = 226013;
            tableRow.Append(CreateTextCell("BUILDING INFORMATION", "D8DDEC", 10, "000000", 2, true));
            tableRow.Append(CreateTextCell("", "D8DDEC", 10, "000000", 1, true));
            tableRow.Append(CreateTextCell("LEASE INFORMATION", "D8DDEC", 10, "000000", 2, true));
            tableRow.Append(CreateTextCell("", "D8DDEC", 10, "000000", 1, true));
            table.Append(tableRow);
        }

        private static void AddOwnerRow(D.Table table, Dictionary<string, string> headerAndValue, string backgroundColor, int fontSize, string fontColor, bool isBold)
        {
            D.TableRow tableRow = new D.TableRow();
            tableRow.Height = 226013;

            foreach (var item in headerAndValue)
            {
                tableRow.Append(CreateTextCell(item.Key, backgroundColor, fontSize, fontColor, 1, isBold));
                tableRow.Append(CreateTextCell(item.Value, backgroundColor, fontSize, fontColor, 1, isBold));
            }

            table.Append(tableRow);
        }
    }
}
