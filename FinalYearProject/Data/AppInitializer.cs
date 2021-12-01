using FinalYearProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Data.Models
{

    public class AppInitializer
    {
        public static void seed(IApplicationBuilder applicationBuilder)
            {
                using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<mydbcon>();
                    if (!context.Professors.Any())
                    {
                        context.Professors.AddRange(
                            new Professor()
                            {
                                Name = "MohamedAbosri3",
                                Email = "mohamedaf@gmail.com",
                                Password = "mo123456",
                                Isdisabled = false
                            },
                            new Professor()
                            {
                                Name = "ikillhumanity",
                                Email = "ikillhumanity@gmail.com",
                                Password = "mo123456",
                                Isdisabled = false
                            },
                            new Professor()
                            {
                                Name = "asemaljazar",
                                Email = "asemaljazar@gmail.com",
                                Password = "mo123456",
                                Isdisabled = false
                            }
                            );

                    }

                    if (!context.Schedules.Any())
                    {
                        context.Schedules.AddRange(
                            new Schedule()
                            {
                                Name = "AI",
                                Description = "the AI exam will be at ",
                                Time = new DateTime(2022, 05, 09, 9, 15, 00)

                            },
                            new Schedule()
                            {
                                Name = "Informatics",
                                Description = "the Informatics exam will be at ",
                                Time = new DateTime(2022, 05, 09, 12, 15, 00)

                            },
                            new Schedule()
                            {
                                Name = "Network",
                                Description = "the Network exam will be at ",
                                Time = new DateTime(2022, 05, 08, 12, 15, 00)
                            },
                            new Schedule()
                            {
                                Name = "ComputerGraphics",
                                Description = "the Computer Graphics exam will be at ",
                                Time = new DateTime(2022, 05, 08, 10, 15, 00)
                            }



                            );

                    };
                    if (!context.Courses.Any())
                    {
                        context.Courses.AddRange(
                            new Course()
                            {
                                Name = "AI",
                                CreditHrs = 4,
                                ProfessorId = 2,
                                ScheduleId = 3
                            },
                             new Course()
                             {
                                 Name = "ComputerGraphics",
                                 CreditHrs = 3,
                                 ProfessorId = 4,
                                 ScheduleId = 6
                             },
                              new Course()
                              {
                                  Name = "Informatics",
                                  CreditHrs = 3,
                                  ProfessorId = 3,
                                  ScheduleId = 4
                              },
                               new Course()
                               {
                                   Name = "Network",
                                   CreditHrs = 4,
                                   ProfessorId = 2,
                                   ScheduleId = 5
                               }
                            );
                        
                    }
                    context.SaveChanges();

                }

            }
    
        public void addQuestionsIbrahiem()
        {
            //if (!context.Questions.Any())
            //{
            //    context.Questions.AddRange(
            //        new Question()
            //        {
            //            Question1 = "The World Wide Web was not viable to the general public until ________.",
            //            Answer = "{'A':'1980','B':'1990','C':'1995','D':'1993'}",
            //            Hint = null,
            //            Goal = "D",
            //            CourseId = 1,
            //            Difficulty = "Hard"
            //        },
            //        new Question()
            //        {
            //            Question1 = "In the 1980s, only large universities and ________ were able to access the Internet including e - mail.",
            //            Answer = "{'A':'news organizations','B':'TV stations','C':'the U.S. government','D':'corporations'}",
            //            Hint = null,
            //            Goal = "C",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "A printer is classified as a(n) ________.",
            //            Answer = "{'A':' integral part of every computer','B':'processing device','C':' output device','D':'system device'}",
            //            Hint = null,
            //            Goal = "C",
            //            CourseId = 1,
            //            Difficulty = "Hard"
            //        },
            //        new Question()
            //        {
            //            Question1 = "Which item below is not part of the computer IPOS cycle?",
            //            Answer = "{'A':'Information','B':'Processing','C':'Output','D':'Storage'}",
            //            Hint = null,
            //            Goal = "A",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "________ software controls all devices and operations completed by a computer.",
            //            Answer = "{'A':'Major','B':'Application','C':'Program','D':'System'}",
            //            Hint = null,
            //            Goal = "D",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = " A series of steps that describe what a computer must do to solve a problem or perform a task is a(n) ________.",
            //            Answer = "{'A':'system procedure','B':'function','C':'scenario','D':'algorithm'}",
            //            Hint = null,
            //            Goal = "D",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "Which of the below is NOT a(n) output device?",
            //            Answer = "{'A':'Speaker','B':'Monitor','C':'Microphone','D':'Printer'}",
            //            Hint = null,
            //            Goal = "C",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "Information stored in a computer's RAM is ________.",
            //            Answer = "{'A':'permanent','B':'non-volatile','C':'temporary','D':'slower than secondary memory'}",
            //            Hint = null,
            //            Goal = "C",
            //            CourseId = 1,
            //            Difficulty = "Hard"
            //        },
            //        new Question()
            //        {
            //            Question1 = "If you assign different user names for each user of your computer, the operating system creates a special ________ for each user.",
            //            Answer = "{'A':'network','B':'disk drive','C':'profile','D':'email account'}",
            //            Hint = null,
            //            Goal = "C",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "Windows and Macs are equipped with ________ capabilities to allow different devices to automatically be recognized by the system.",
            //            Answer = "{'A':'plug-and-play','B':' library modules','C':'special nodes','D':'device recognition'}",
            //            Hint = null,
            //            Goal = "A",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "When one task uses its ________ or is interrupted by a task of higher priority, the task is suspended and the other task starts.",
            //            Answer = "{'A':'CPA','B':'RAM','C':'buffer','D':'time slice'}",
            //            Hint = null,
            //            Goal = "D",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "To ensure that programs run quickly, operating systems use the computer's RAM as a(n) ________.",
            //            Answer = "{'A':'accelerator','B':'command enhancer','C':'buffer','D':'quick access system'}",
            //            Hint = null,
            //            Goal = "C",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "Starting more than one application at a time is known as ________.",
            //            Answer = "{'A':'multitasking','B':'convergence','C':'loading foreground','D':'workload increase'}",
            //            Hint = null,
            //            Goal = "A",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "Program instructions and data are divided into fixed sized units called ________.",
            //            Answer = {"A":"blocks","B":"batches","C":"pages","D":"packets"},
            //            Hint = null,
            //            Goal = "C",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "Press [Alt]+[Ctrl]+[Delete] to access the Windows ________.",
            //            Answer = "{'A':'processor','B':' Task Manager','C':'shut-down sequence','D':'print function'}",
            //            Hint = null,
            //            Goal = "B",
            //            CourseId = 1,
            //            Difficulty = "Moderate"
            //        },
            //        new Question()
            //        {
            //            Question1 = "When Task Manager is visible, select the ________ tab to view a list of processes running.",
            //            Answer = "{'A':'Processes','B':'Procedures','C':'Programs','D':'People'}",
            //            Hint = null,
            //            Goal = "A",
            //            CourseId = 1,
            //            Difficulty = "Moderate"
            //        },
            //        new Question()
            //        {
            //            Question1 = "The status bar of the Task Manager provides information about the ________ and memory usage.",
            //            Answer = "{'A':'disk drive','B':'input/output','C':'CPU','D':'printer'}",
            //            Hint = null,
            //            Goal = "C",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "To enable a printer to keep up with the amount of data sent to it by the computer, a ________ program is used.",
            //            Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
            //            Hint = null,
            //            Goal = "D",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "A type of computer operating system user interface that is commonly used today is ________.",
            //            Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
            //            Hint = null,
            //            Goal = "D",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "The lower edge of the PC desktop screen displays a ________.",
            //            Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
            //            Hint = null,
            //            Goal = "D",
            //            CourseId = 1,
            //            Difficulty = "Easy"
            //        },
            //        new Question()
            //        {
            //            Question1 = "In a command line operating system, the command copy C:\myfile.txt F:\myfile.txt will copy ________.",
            //            Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
            //            Hint = null,
            //            Goal = "A",
            //            CourseId = 1,
            //            Difficulty = "Hard"
            //        },
            //        new Question()
            //        {
            //            Question1 = " ________ is a feature that enables subtle animations and translucent glass windows that can be custom colored.",
            //            Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
            //            Hint = null,
            //            Goal = "A",
            //            CourseId = 1,
            //            Difficulty = "Hard"
            //        },
            //        new Question()
            //        {
            //            Question1 = "An operating system available at no cost is the ________ operating system.",
            //            Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
            //            Hint = null,
            //            Goal = "C",
            //            CourseId = 1,
            //            Difficulty = "Hard"
            //        },
            //        new Question()
            //        {
            //            Question1 = "The original Macintosh operating system called Mac OS was released in:",
            //            Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
            //            Hint = null,
            //            Goal = "C",
            //            CourseId = 1,
            //            Difficulty = "Hard"
            //        },
            //        new Question()
            //        {
            //            Question1 = " The Linux operating system called ________ was released to promote One Laptop per Child.",
            //            Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
            //            Hint = null,
            //            Goal = "C",
            //            CourseId = 1,
            //            Difficulty = "Hard"
            //        },
            //        new Question()
            //        {
            //            Question1 = " Windows dominates the market for operating systems at about ________ percent.",
            //            Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
            //            Hint = null,
            //            Goal = "B",
            //            CourseId = 1,
            //            Difficulty = "Hard"
            //        },




            //  );
        }
        public void addQuestionToCourse(dynamic context,int c_id)
        {
            if (!context.Questions.Any())
            {
                context.Questions.AddRange(
                    new Question()
                    {
                        Question1 = "The World Wide Web was not viable to the general public until ________.",
                        Answer = "{'A':'1980','B':'1990','C':'1995','D':'1993'}",
                        Hint = null,
                        Goal = "D",
                        CourseId = 1,
                        Difficulty = "Hard"
                    },
                    new Question()
                    {
                        Question1 = "The World Wide Web was not viable to the general public until ________.",
                        Answer = "{'A':'1980','B':'1990','C':'1995','D':'1993'}",
                        Hint = null,
                        Goal = "D",
                        CourseId = 1,
                        Difficulty = "Hard"
                    }
               );
            }

        }
    }
}
    


