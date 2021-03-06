﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.CloudPainters;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization.Visualization
{
    public class TagCloudVisualizer : ICloudVisualizer<Tuple<string, Rectangle>>
    {
        private readonly VisualisingOptions visualisingOptions;

        public TagCloudVisualizer(VisualisingOptions visualisingOptions)
        {
            this.visualisingOptions = visualisingOptions;
        }

        public Bitmap GetVisualization(IEnumerable<string> words, ILayouter layouter,
            ICloudPainter<Tuple<string, Rectangle>> cloudPainter)
        {
            var rectangles = GetRectanglesForWords(words, layouter);
            return cloudPainter.GetImage(words.Zip(rectangles, Tuple.Create), visualisingOptions);
        }
        
        private IEnumerable<Rectangle> GetRectanglesForWords(IEnumerable<string> words, ILayouter layouter)
        {
            return words.Select(word =>
                layouter.PutNextRectangle(GetWordSize(word, visualisingOptions.Font, visualisingOptions.ImageSize)));
        }

        private Size GetWordSize(string word, Font font, Size pictureSize)
        {
            //return TextRenderer.MeasureText(word, font);
            var bitmap = new Bitmap(pictureSize.Width, pictureSize.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                return graphics.MeasureString(word, font).ToSize();
            }
        }
    }
}