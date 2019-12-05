﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.CloudPainters;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization.Visualization
{
    public class TagCloudVisualizer
    {
        private readonly VisualisingOptions visualisingOptions;

        public TagCloudVisualizer(VisualisingOptions visualisingOptions)
        {
            this.visualisingOptions = visualisingOptions;
        }

        public Bitmap GetVisualization(IEnumerable<string> words, ILayouter layouter, CloudPainter cloudPainter)
        {
            var rectangles = GetRectanglesForWords(words, layouter);
            return cloudPainter.GetImage(words, rectangles, visualisingOptions);
        }

        private IEnumerable<Rectangle> GetRectanglesForWords(IEnumerable<string> words, ILayouter layouter)
        {
            return words.Select(word =>
                layouter.PutNextRectangle(GetWordSize(word, visualisingOptions.Font, visualisingOptions.ImageSize)));
        }

        private Size GetWordSize(string word, Font font, Size pictureSize)
        {
            var bitmap = new Bitmap(pictureSize.Width, pictureSize.Height);
            var image = Graphics.FromImage(bitmap);
            return image.MeasureString(word, font).ToSize();
        }
    }
}