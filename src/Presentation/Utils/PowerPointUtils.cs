using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using D = DocumentFormat.OpenXml.Drawing;
using W = DocumentFormat.OpenXml.Wordprocessing;

namespace apollo.Presentation.Utils
{
    public static class PowerPointUtils
    {
        public static IEnumerable<SlidePart> GetSlidePartsInOrder(this PresentationPart presentationPart)
        {
            SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;

            return slideIdList.ChildElements
                .Cast<SlideId>()
                .Select(x => presentationPart.GetPartById(x.RelationshipId))
                .Cast<SlidePart>();
        }

        public static SlidePart CloneSlide(this SlidePart templatePart)
        {
            // find the presentationPart: makes the API more fluent
            var presentationPart = templatePart.GetParentParts()
                .OfType<PresentationPart>()
                .Single();

            // clone slide contents
            Slide currentSlide = (Slide)templatePart.Slide.CloneNode(true);
            var slidePartClone = presentationPart.AddNewPart<SlidePart>();
            currentSlide.Save(slidePartClone);

            // copy layout part
            slidePartClone.AddPart(templatePart.SlideLayoutPart);

            return slidePartClone;
        }

        public static void AppendSlide(this PresentationPart presentationPart, SlidePart newSlidePart)
        {
            SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;

            // find the highest id
            uint maxSlideId = slideIdList.ChildElements
                .Cast<SlideId>()
                .Max(x => x.Id.Value);

            // Insert the new slide into the slide list after the previous slide.
            var id = maxSlideId + 1;

            SlideId newSlideId = new SlideId();
            slideIdList.Append(newSlideId);
            newSlideId.Id = id;
            newSlideId.RelationshipId = presentationPart.GetIdOfPart(newSlidePart);
        }

        // Insert the specified slide into the presentation at the specified position.
        public static void InsertNewSlide(PresentationDocument presentationDocument, SlidePart slidePart, int position)
        {

            if (presentationDocument == null)
            {
                throw new ArgumentNullException("presentationDocument");
            }
            PresentationPart presentationPart = presentationDocument.PresentationPart;

            // Verify that the presentation is not empty.
            if (presentationPart == null)
            {
                throw new InvalidOperationException("The presentation document is empty.");
            }


            // Modify the slide ID list in the presentation part.
            // The slide ID list should not be null.
            SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;

            // Find the highest slide ID in the current list.
            uint maxSlideId = 1;
            SlideId prevSlideId = null;

            foreach (SlideId slideId in slideIdList.ChildElements.Cast<SlideId>())
            {
                if (slideId.Id > maxSlideId)
                {
                    maxSlideId = slideId.Id;
                }

                position--;
                if (position == 0)
                {
                    prevSlideId = slideId;
                }

            }

            maxSlideId++;

            // Insert the new slide into the slide list after the previous slide.
            SlideId newSlideId = slideIdList.InsertAfter(new SlideId(), prevSlideId);
            newSlideId.Id = maxSlideId;
            newSlideId.RelationshipId = presentationPart.GetIdOfPart(slidePart);
        }

        public static SlideLayoutPart GetLayoutPartWithName(PresentationPart presentationPart, string layoutName)
        {
            // Get SlideMasterPart and SlideLayoutPart from the existing Presentation Part
            SlideMasterPart slideMasterPart = presentationPart.SlideMasterParts.First();
            if (slideMasterPart == null) return null;
            SlideLayoutPart slideLayoutPart = slideMasterPart.SlideLayoutParts.SingleOrDefault
                (sl => sl.SlideLayout.CommonSlideData.Name.Value.Equals(layoutName, StringComparison.OrdinalIgnoreCase));
            if (slideLayoutPart == null)
            {
                return null;
            }

            return slideLayoutPart;
        }

        public static SlidePart GetNewSlideWIthLayout(PresentationPart presentationPart, string layoutName)
        {
            Slide slide = new Slide();
            SlidePart slidePart = presentationPart.AddNewPart<SlidePart>();
            slide.Save(slidePart);

            SlideMasterPart slideMasterPart = presentationPart.SlideMasterParts.FirstOrDefault();
            SlideLayoutPart slideLayoutPart = slideMasterPart.GetSlideLayoutPartByLayoutName(layoutName);

            if (slideLayoutPart == null)
            {
                return null;
            }

            string id = slideMasterPart.GetIdOfPart(slideLayoutPart);
            slidePart.CloneSlideLayout(slideLayoutPart, id);

            return slidePart;
        }

        public static void SetSlideID(PresentationPart presentationPart, SlidePart slidePart, int? position = null)
        {
            SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;
            if (slideIdList == null)
            {
                slideIdList = new SlideIdList();
                presentationPart.Presentation.SlideIdList = slideIdList;
            }

            if (position != null && position > slideIdList.Count())
                throw new InvalidOperationException($"Unable to set slide to position '{position}'. There are only '{slideIdList.Count()}' slides.");

            uint newId = slideIdList.ChildElements.Count() == 0 ? 256 : slideIdList.GetMaxSlideId() + 1;
            if (position == null)
            {
                var newSlideId = slideIdList.AppendChild(new SlideId());
                newSlideId.Id = newId;
                newSlideId.RelationshipId = presentationPart.GetIdOfPart(slidePart);
            }
            else
            {
                SlideId nextSlideId = (SlideId)slideIdList.ChildElements[position.Value - 1];
                var newSlideId = slideIdList.InsertBefore(new SlideId(), nextSlideId);
                newSlideId.Id = newId;
                newSlideId.RelationshipId = presentationPart.GetIdOfPart(slidePart);
            }
        }

        public static uint GetMaxSlideId(this SlideIdList slideIdList)
        {
            uint maxSlideId = 0;
            if (slideIdList.ChildElements.Count() > 0)
                maxSlideId = slideIdList.ChildElements
                    .Cast<SlideId>()
                    .Max(x => x.Id.Value);
            return maxSlideId;
        }

        public static SlideLayoutPart GetSlideLayoutPartByLayoutName(this SlideMasterPart slideMasterPart, string layoutName)
        {
            return slideMasterPart.SlideLayoutParts.SingleOrDefault
                    (sl => sl.SlideLayout.CommonSlideData.Name.Value.Equals(layoutName, StringComparison.OrdinalIgnoreCase));
        }

        public static void CloneSlideLayout(this SlidePart newSlidePart, SlideLayoutPart slPart, string id)
        {
            /* ensure we added the rel ID to this part */
            newSlidePart.AddPart(slPart, id);
            newSlidePart.Slide.CommonSlideData = (CommonSlideData)slPart.SlideLayout.CommonSlideData.Clone();

            var graphicFrameLayouts = slPart.SlideLayout.CommonSlideData.ShapeTree.Descendants<GraphicFrame>();

            foreach (var item in graphicFrameLayouts)
            {
                GraphicFrame graphicFrameSlide = (GraphicFrame)item.CloneNode(true);
                newSlidePart.Slide.CommonSlideData.ShapeTree.AppendChild(graphicFrameSlide);
            }

            foreach (ImagePart iPart in slPart.ImageParts)
                newSlidePart.AddPart(iPart, slPart.GetIdOfPart(iPart));
        }

        public static void AddNewParagraph(Shape shape, string NewText)
        {
            D.Paragraph p = new D.Paragraph();
            TextBody docBody = shape.TextBody;
            docBody.BodyProperties = new D.BodyProperties { UpRight = true };
            W.Justification justification1 = new W.Justification() { Val = W.JustificationValues.Start };
            p.ParagraphProperties = new D.ParagraphProperties(justification1);
            D.Run run = new D.Run(new D.Text(NewText));
            D.RunProperties runProp = shape.Descendants<D.RunProperties>().FirstOrDefault();
            run.AppendChild(runProp);
            D.Text newText = new D.Text(NewText);
            run.AppendChild(newText);
            p.Append(run);
            docBody.Append(p);
        }

        public static D.Paragraph CreateStyledParagraph(string text, System.Drawing.Color color, bool isBold, bool isItalic, int fontSize = 2000)
        {
            var runProperties = new D.RunProperties(); //set basic styles for paragraph
            runProperties.Bold = isBold;
            runProperties.Italic = isItalic;
            runProperties.FontSize = fontSize;
            runProperties.Dirty = false;

            var hexColor = color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");//convert color to hex
            var solidFill = new D.SolidFill();
            var rgbColorModelHex = new D.RgbColorModelHex() { Val = hexColor };

            solidFill.Append(rgbColorModelHex);
            runProperties.Append(solidFill);

            //use this to assign the font family
            runProperties.Append(new D.LatinFont() { Typeface = "Arial Black" });

            var textBody = new D.Text();
            textBody.Text = text; //assign text

            var run = new D.Run();
            var newParagraph = new D.Paragraph();

            run.Append(runProperties);//append styles
            run.Append(textBody);//append text
            newParagraph.Append(run);//append run to paragraph

            return newParagraph;
        }


        internal static Picture AddPicture(this Slide slide, string imageFile)
        {
            Picture picture = new Picture();

            string embedId = string.Empty;
            UInt32Value picId = 10001U;
            string name = string.Empty;

            if (slide.Elements<Picture>().Count() > 0)
            {
                picId = ++slide.Elements<Picture>().ToList().Last().NonVisualPictureProperties.NonVisualDrawingProperties.Id;
            }
            name = "image" + picId.ToString();
            embedId = "rId" + "14523";

            NonVisualPictureProperties nonVisualPictureProperties = new NonVisualPictureProperties()
            {
                NonVisualDrawingProperties = new NonVisualDrawingProperties() { Name = name, Id = picId, Title = name },
                NonVisualPictureDrawingProperties = new NonVisualPictureDrawingProperties() { PictureLocks = new D.PictureLocks() { NoChangeAspect = true } },
                ApplicationNonVisualDrawingProperties = new ApplicationNonVisualDrawingProperties() { UserDrawn = true }
            };

            BlipFill blipFill = new BlipFill() { Blip = new D.Blip() { Embed = embedId } };
            D.Stretch stretch = new D.Stretch() { FillRectangle = new D.FillRectangle() };
            blipFill.Append(stretch);

            ShapeProperties shapeProperties = new ShapeProperties()
            {
                Transform2D = new D.Transform2D()
                {
                    Offset = new D.Offset() { X = 3529831L, Y = 1247458L },
                    Extents = new D.Extents() { Cx = 8275261, Cy = 5073764 }
                }
            };
            D.PresetGeometry presetGeometry = new D.PresetGeometry() { Preset = D.ShapeTypeValues.Rectangle };
            D.AdjustValueList adjustValueList = new D.AdjustValueList();

            presetGeometry.Append(adjustValueList);
            shapeProperties.Append(presetGeometry);
            picture.Append(nonVisualPictureProperties);
            picture.Append(blipFill);
            picture.Append(shapeProperties);

            slide.CommonSlideData.ShapeTree.Append(picture);

            // Add Image part
            slide.AddImagePart(embedId, imageFile);

            slide.Save();
            return picture;
        }

        private static void AddImagePart(this Slide slide, string relationshipId, string imageFile)
        {
            ImagePart imgPart = slide.SlidePart.AddImagePart(ImagePartType.Png, relationshipId);
            using (FileStream imgStream = File.OpenRead(imageFile))
            {
                imgPart.FeedData(imgStream);
            }
        }

        private static D.TableCell FindTableCell(Slide slide, int tableIndex, int rowIndex, int colIndex)
        {
            var graphicFrame = slide.CommonSlideData.ShapeTree.Descendants<GraphicFrame>().ElementAt(tableIndex);
            var table = graphicFrame.Descendants<D.Table>().First();
            var row = table.Descendants<D.TableRow>().ElementAt(rowIndex);
            var cell = row.Descendants<D.TableCell>().ElementAt(colIndex);


            //D.Table table = newSlidePart.Slide.Descendants<D.Table>().FirstOrDefault();

            //if (table != null)
            //{
            //    // Find the cell you want to update
            //    D.TableCell cell = table.Descendants<D.TableCell>()
            //        .Where(c => c.InnerText == "AMENITIES")
            //        .FirstOrDefault();

            //    var cellTextBody = cell.FirstChild;
            //    var cellParaGraph = cellTextBody.Descendants<D.Paragraph>().FirstOrDefault();
            //    var cellRun = cellParaGraph.Descendants<D.Run>().First();

            //    // Update the text content of the run
            //    var cellText = cellRun.Descendants<D.Text>().First();
            //    cellText.Text = "ABC Unit";
            //    newSlidePart.Slide.Save();
            //    doc.Save();
            //}


            return cell;
        }

        private static void UpdateTableCellText(D.TableCell cell, string newText)
        {
            // Remove existing text
            var textBody = cell.TextBody;
            textBody.RemoveAllChildren<D.Paragraph>();

            // Add new text
            var paragraph = new D.Paragraph();
            var run = new D.Run();
            var text = new D.Text { Text = newText };
            run.Append(text);
            paragraph.Append(run);
            textBody.Append(paragraph);
        }


        private static D.TableRow CreateCustomRow(int numCols, System.Drawing.Color backgroundColor, int fontSize, System.Drawing.Color color)
        {
            D.TableRow tableRow = new D.TableRow();

            for (int col = 0; col < numCols; col++)
            {
                D.TableCell tableCell = new D.TableCell();

                // Set the cell background color
                D.TableCellProperties tableCellProperties = new D.TableCellProperties();

                var backgroundColorHex = backgroundColor.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");//convert color to hex
                var backgroundColorSolidFill = new D.SolidFill();
                var backgroundColorRgbHex = new D.RgbColorModelHex() { Val = backgroundColorHex };

                backgroundColorSolidFill.Append(backgroundColorRgbHex);
                tableCellProperties.Append(backgroundColorSolidFill);
                tableCell.Append(tableCellProperties);

                // Create a TextBody with custom font size and color
                D.TextBody textBody = new D.TextBody();
                D.BodyProperties bodyProperties = new D.BodyProperties();
                textBody.Append(bodyProperties);

                D.ListStyle listStyle = new D.ListStyle();
                textBody.Append(listStyle);

                D.Paragraph paragraph = new D.Paragraph();
                D.Run run = new D.Run();
                D.RunProperties runProperties = new D.RunProperties
                {
                    FontSize = fontSize * 100, // Font size in hundredths of a point
                };

                var hexColor = color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");//convert color to hex
                var solidFill = new D.SolidFill();
                var rgbColorModelHex = new D.RgbColorModelHex() { Val = hexColor };

                solidFill.Append(rgbColorModelHex);
                runProperties.Append(solidFill);

                run.Append(runProperties);
                D.Text text = new D.Text { Text = $"Cell {col + 1}" };
                run.Append(text);
                paragraph.Append(run);
                tableCell.Append(paragraph);
                tableRow.Append(tableCell);
            }

            return tableRow;
        }

    }
}
