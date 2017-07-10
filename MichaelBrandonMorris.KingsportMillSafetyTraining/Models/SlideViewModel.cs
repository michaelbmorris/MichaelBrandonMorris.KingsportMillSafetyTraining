using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class SlideViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for SlideViewModel
    public class SlideViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="SlideViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public SlideViewModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="SlideViewModel" /> class.
        /// </summary>
        /// <param name="slide">The slide.</param>
        /// TODO Edit XML Comment Template for #ctor
        public SlideViewModel(Slide slide)
        {
            if (slide == null)
            {
                return;
            }

            Answers = slide.Answers;
            CategoryId = slide.Category.Id;
            CategoryTitle = slide.Category.Title;
            Content = slide.Content;
            CorrectAnswerIndex = slide.CorrectAnswerIndex;
            Id = slide.Id;
            ImageBytes = slide.ImageBytes;
            ImageDescription = slide.ImageDescription;
            Index = slide.Index;
            ShouldShowImageOnQuiz = slide.ShouldShowImageOnQuiz;
            ShouldShowQuestionOnQuiz = slide.ShouldShowQuestionOnQuiz;
            ShouldShowSlideInSlideshow = slide.ShouldShowSlideInSlideshow;
            Title = slide.Title;
            Question = slide.Question;
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="SlideViewModel" /> class.
        /// </summary>
        /// <param name="slide">The slide.</param>
        /// <param name="categories">The categories.</param>
        /// TODO Edit XML Comment Template for #ctor
        public SlideViewModel(Slide slide, IList<Category> categories)
            : this(slide)
        {
            Categories = categories;
        }

        /// <summary>
        ///     Gets or sets the answers.
        /// </summary>
        /// <value>The answers.</value>
        /// TODO Edit XML Comment Template for Answers
        public IList<Answer> Answers
        {
            get;
            set;
        } = new List<Answer>();

        /// <summary>
        ///     Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        /// TODO Edit XML Comment Template for Categories
        public IList<Category> Categories
        {
            get;
            set;
        } = new List<Category>();

        /// <summary>
        ///     Gets or sets the category identifier.
        /// </summary>
        /// <value>The category identifier.</value>
        /// TODO Edit XML Comment Template for CategoryId
        [Display(Name = "Category")]
        public int CategoryId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the category title.
        /// </summary>
        /// <value>The category title.</value>
        /// TODO Edit XML Comment Template for CategoryTitle
        [Display(Name = "Category")]
        public string CategoryTitle
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        /// TODO Edit XML Comment Template for Content
        [AllowHtml]
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the index of the correct answer.
        /// </summary>
        /// <value>The index of the correct answer.</value>
        /// TODO Edit XML Comment Template for CorrectAnswerIndex
        public int CorrectAnswerIndex
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// TODO Edit XML Comment Template for Id
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        /// TODO Edit XML Comment Template for Image
        public HttpPostedFileBase Image
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the image bytes.
        /// </summary>
        /// <value>The image bytes.</value>
        /// TODO Edit XML Comment Template for ImageBytes
        [Display(Name = "Image")]
        public byte[] ImageBytes
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the image description.
        /// </summary>
        /// <value>The image description.</value>
        /// TODO Edit XML Comment Template for ImageDescription
        [Display(Name = "Image Description")]
        public string ImageDescription
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        /// TODO Edit XML Comment Template for Index
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the question.
        /// </summary>
        /// <value>The question.</value>
        /// TODO Edit XML Comment Template for Question
        public string Question
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [should show
        ///     image on quiz].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [should show image on quiz];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for ShouldShowImageOnQuiz
        [Display(Name = "Show Image on Quiz?")]
        public bool ShouldShowImageOnQuiz
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [should show
        ///     question on quiz].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [should show question on quiz];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for ShouldShowQuestionOnQuiz
        [Display(Name = "Show Question on Quiz?")]
        public bool ShouldShowQuestionOnQuiz
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [should show
        ///     slide in slideshow].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [should show slide in slideshow];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for ShouldShowSlideInSlideshow
        [Display(Name = "Show Slide in Slideshow?")]
        public bool ShouldShowSlideInSlideshow
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        /// TODO Edit XML Comment Template for Title
        public string Title
        {
            get;
            set;
        }
    }
}