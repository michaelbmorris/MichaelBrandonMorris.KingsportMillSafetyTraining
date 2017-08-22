using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models
{
    using System.Web.Mvc;

    /// <summary>
    /// Class Slide.
    /// </summary>
    /// <seealso cref="IEquatable{T}" />
    /// TODO Edit XML Comment Template for Slide
    public class Slide : IEntity<int>, IEquatable<Slide>
    {
        /// <summary>
        /// Gets or sets the answers.
        /// </summary>
        /// <value>The answers.</value>
        /// TODO Edit XML Comment Template for Answers
        public virtual IList<Answer> Answers
        {
            get;
            set;
        } = new List<Answer>();

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        /// TODO Edit XML Comment Template for Category
        public virtual Category Category
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        /// TODO Edit XML Comment Template for Content
        [AllowHtml]
        [Required]
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the index of the correct answer.
        /// </summary>
        /// <value>The index of the correct answer.</value>
        /// TODO Edit XML Comment Template for CorrectAnswerIndex
        [Display(Name = "Correct Answer Index")]
        public int CorrectAnswerIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// TODO Edit XML Comment Template for Id
        [Key]
        [Column(Order = 0)]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the image bytes.
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
        /// Gets or sets the image description.
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
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        /// TODO Edit XML Comment Template for Index
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        /// <value>The question.</value>
        /// TODO Edit XML Comment Template for Question
        public string Question
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [should show image on quiz].
        /// </summary>
        /// <value><c>true</c> if [should show image on quiz]; otherwise, <c>false</c>.</value>
        /// TODO Edit XML Comment Template for ShouldShowImageOnQuiz
        [Display(Name = "Show Image on Quiz?")]
        [Required]
        public bool ShouldShowImageOnQuiz
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [should show question on quiz].
        /// </summary>
        /// <value><c>true</c> if [should show question on quiz]; otherwise, <c>false</c>.</value>
        /// TODO Edit XML Comment Template for ShouldShowQuestionOnQuiz
        [Display(Name = "Show Question on Quiz?")]
        [Required]
        public bool ShouldShowQuestionOnQuiz
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [should show slide in slideshow].
        /// </summary>
        /// <value><c>true</c> if [should show slide in slideshow]; otherwise, <c>false</c>.</value>
        /// TODO Edit XML Comment Template for ShouldShowSlideInSlideshow
        [Display(Name = "Show Slide in Slideshow?")]
        [Required]
        public bool ShouldShowSlideInSlideshow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        /// TODO Edit XML Comment Template for Title
        [Required]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        /// TODO Edit XML Comment Template for Equals
        public bool Equals(Slide other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id;
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        /// TODO Edit XML Comment Template for ==
        public static bool operator ==(Slide x, Slide y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if ((object) x == null
                || (object) y == null)
            {
                return false;
            }

            return x.Id == y.Id;
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        /// TODO Edit XML Comment Template for !=
        public static bool operator !=(Slide x, Slide y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        /// TODO Edit XML Comment Template for Equals
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((Slide) obj);
        }

        /// <summary>
        /// The mime type for the image.
        /// </summary>
        public string MimeType
        {
            get;
            set;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// TODO Edit XML Comment Template for GetHashCode
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}