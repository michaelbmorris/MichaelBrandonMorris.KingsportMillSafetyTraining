using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MichaelBrandonMorris.Extensions.OtherExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.ViewModels;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data
{
    /// <summary>
    ///     Slides are the basic content element of the safety training program.
    ///     They have text content, an image, and a question with multiple choice
    ///     answers about the content or image. They are assigned to a category.
    /// </summary>
    public class Slide : IEquatable<Slide>
    {
        /// <summary>
        ///     Tracks the current index being used by slides.
        /// </summary>
        public static int CurrentIndex;

        public Slide(SlideViewModel slideViewModel)
            : this()
        {
            Content = slideViewModel.Content;
            CorrectAnswerIndex = slideViewModel.CorrectAnswerIndex;

            if (slideViewModel.Image != null)
                ImageBytes = slideViewModel.Image.ToBytes();

            ImageDescription = slideViewModel.ImageDescription;
            Question = slideViewModel.Question;
            ShouldShowImageOnQuiz = slideViewModel.ShouldShowImageOnQuiz;
            ShouldShowQuestionOnQuiz = slideViewModel.ShouldShowQuestionOnQuiz;

            ShouldShowSlideInSlideshow =
                slideViewModel.ShouldShowSlideInSlideshow;

            Title = slideViewModel.Title;
        }

        /// <summary>
        ///     Creates a new <see cref="Slide" /> with the next <see cref="Index" />.
        /// </summary>
        public Slide()
        {
            Index = ++CurrentIndex;
        }

        /// <summary>
        ///     The slide's children answers
        /// </summary>
        [ListQuantity(
            0,
            5,
            ErrorMessage = "There may be no more than five (5) answers.")]
        public virtual IList<Answer> Answers { get; set; } = new List<Answer>();

        /// <summary>
        ///     The slide's parent category
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        ///     The slide's text content
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        ///     The index of the slide's correct answer
        /// </summary>
        [Display(Name = "Correct Answer Index")]
        public int CorrectAnswerIndex { get; set; }

        /// <summary>
        ///     The slide's ID, generated automatically by the database
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        /// <summary>
        ///     The slide's image, stored as an array of bytes
        /// </summary>
        [Display(Name = "Image")]
        public byte[] ImageBytes { get; set; }

        /// <summary>
        ///     The slide's image's description, used for alternate text
        /// </summary>
        [Display(Name = "Image Description")]
        public string ImageDescription { get; set; }

        /// <summary>
        ///     The slide's index, which should be unique within the category
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        ///     The slide's question
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        ///     Whether or not the slide's image should be displayed on the quiz
        ///     Value is irrelevant if <see cref="ShouldShowQuestionOnQuiz" /> is
        ///     false or <see cref="ShouldShowSlideInSlideshow" /> is false
        /// </summary>
        [Display(Name = "Show Image on Quiz?")]
        [Required]
        public bool ShouldShowImageOnQuiz { get; set; }

        /// <summary>
        ///     Whether or not the slide's question should be displayed on the quiz
        ///     Value is irrelevant if <see cref="ShouldShowSlideInSlideshow" />
        ///     is false
        /// </summary>
        [Display(Name = "Show Question on Quiz?")]
        [Required]
        public bool ShouldShowQuestionOnQuiz { get; set; }

        /// <summary>
        ///     Whether or not the slide should be displayed in the slideshow
        /// </summary>
        [Display(Name = "Show Slide in Slideshow?")]
        [Required]
        public bool ShouldShowSlideInSlideshow { get; set; }

        /// <summary>
        ///     The slide's title
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        ///     Checks for equality with another slide. If the slides have the same
        ///     reference or ID, they are equal.
        /// </summary>
        /// <param name="other">The slide to compare this slide to.</param>
        /// <returns></returns>
        public bool Equals(Slide other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id == other.Id;
        }

        /// <summary>
        ///     Checks for slide equality. First checks for identical slide
        ///     references, and then for identical IDs (primary key).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(Slide x, Slide y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if ((object) x == null || (object) y == null)
                return false;

            return x.Id == y.Id;
        }

        /// <summary>
        ///     Checks for slide inequality. Implemented in terms of the equality
        ///     operator.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(Slide x, Slide y)
        {
            return !(x == y);
        }

        /// <summary>
        ///     Checks for equality with another object. If this slide and the
        ///     object have the same reference, or the object is a slide and passes
        ///     <see cref="Equals(Slide)" />, they are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((Slide) obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}