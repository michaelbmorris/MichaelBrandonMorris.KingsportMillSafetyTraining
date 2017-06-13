using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.DataModels;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.ViewModels
{
    public class SlideViewModel
    {
        public SlideViewModel()
        {
        }

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

        public SlideViewModel(Slide slide, IList<Category> categories)
            : this(slide)
        {
            Categories = categories;
        }

        public IList<Answer> Answers
        {
            get;
            set;
        } = new List<Answer>();

        public IList<Category> Categories
        {
            get;
            set;
        } = new List<Category>();

        public int CategoryId
        {
            get;
            set;
        }

        [Display(Name = "Category")]
        public string CategoryTitle
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public int CorrectAnswerIndex
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public HttpPostedFileBase Image
        {
            get;
            set;
        }

        public byte[] ImageBytes
        {
            get;
            set;
        }

        [Display(Name = "Image Description")]
        public string ImageDescription
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public string Question
        {
            get;
            set;
        }

        [Display(Name = "Show Image on Quiz?")]
        public bool ShouldShowImageOnQuiz
        {
            get;
            set;
        }

        [Display(Name = "Show Question on Quiz?")]
        public bool ShouldShowQuestionOnQuiz
        {
            get;
            set;
        }

        [Display(Name = "Show Slide in Slideshow?")]
        public bool ShouldShowSlideInSlideshow
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }
    }
}