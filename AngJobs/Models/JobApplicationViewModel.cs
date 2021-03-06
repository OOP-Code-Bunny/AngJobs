﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace AngJobs.Models
{
    public class JobApplicationViewModel
    {
        DBContext db = HttpContext.Current.GetOwinContext().Get<DBContext>();

        public JobApplicationViewModel()
        {
        }
        public JobApplicationViewModel(JobApplication entity)
        { 
                id = entity.Id;
                email = entity.ApplicantEmail;
              
                message = entity.ApplicantMessage;
                dateCreated = entity.DateCreated;

                idealJobTitle = entity.JobPost.JobTitle;
        }

        public int id { get; set; }
        [Required(ErrorMessage = "The Applicant email is required.")]
        public string email { get; set; }
        public string skills { get; set; }
        public string experience { get; set; }
        public string message { get; set; }
        [Required(ErrorMessage = "No job post associated to this application.")]
        public int jobPostId { get; set; }
        public string idealJobTitle { get; set; }
        public string idealLocation { get; set; }
        public string idealSalary { get; set; }

        public DateTime? dateCreated { get; set; }
        public Guid? cvGuid { get; set; }

        public JobApplication ToEntity()
        {
            //TODO
           // CV cv = cvGuid.HasValue ? db.CVs.FirstOrDefault(f => f.Guid.Equals(cvGuid.Value)) : null; 
            return new JobApplication
            {
                ApplicantEmail = email,
               
                ApplicantMessage = message,
                JobPost = db.jobPosts.Find(this.jobPostId),
               // CV = cv,
                Ip = HttpContext.Current.Request.UserHostAddress
            };
        }
    }
}