﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public class TrainingResultViewModel
    {
        public TrainingResultViewModel()
        {
        }

        public TrainingResultViewModel(TrainingResult trainingResult)
        {
            CompletionDateTime = trainingResult.CompletionDateTime;
            Id = trainingResult.Id;
            QuizResults = trainingResult.QuizResults;
            RoleTitle = trainingResult.Role.Title;
            TimeToComplete = trainingResult.TimeToComplete;
            var user = trainingResult.User;
            CompanyName = user.CompanyName;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
            UserId = user.Id;
        }

        [DisplayName("Number of Quiz Attempts")]
        public int QuizAttemptsCount => QuizResults.Count;

        [DisplayName("Company")]
        public string CompanyName
        {
            get;
            set;
        }

        [DisplayName("Completed On")]
        public DateTime? CompletionDateTime
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        [DisplayName("Phone Number")]
        public string PhoneNumber
        {
            get;
            set;
        }

        [DisplayName("First Name")]
        public string FirstName
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        [DisplayName("Last Name")]
        public string LastName
        {
            get;
            set;
        }

        [DisplayName("Quiz Results")]
        public IList<QuizResult> QuizResults
        {
            get;
            set;
        }

        [DisplayName("Role")]
        public string RoleTitle
        {
            get;
            set;
        }
       
        internal TimeSpan TimeToComplete
        {
            get;
            set;
        }

        [DisplayName("Time to Complete")]
        public string TimeToCompleteString =>
            $"{TimeToComplete.TotalMinutes:#.##} Minutes";

        public string UserId
        {
            get;
            set;
        }
    }
}