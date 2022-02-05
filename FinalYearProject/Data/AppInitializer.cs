using FinalYearProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace FinalYearProject.Data.Models
{

    public class AppInitializer
    {
        public static void seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<mydbcon>();
                if (!context.Faculties.Any())
                {
                    context.Faculties.AddRange(
                        new Faculty()
                        {
                            Name = "IT"
                        },
                        new Faculty()
                        {
                            Name = "Engineering"
                        },
                        new Faculty()
                        {
                            Name = "Dentistry"
                        });
                }

                if (!context.ApplicationUsers.Any())
                {
                    context.ApplicationUsers.AddRange(
                        new ApplicationUser()
                        {
                            firstname = "mohamed",
                            lastname = "abosri3",
                            UserName = "MohamedAbosrea",
                            Email = "mohamedaf@gmail.com",
                            PasswordHash = "mo123456",
                            LockoutEnabled = false,
                            FacultyId = 2,
                            NormalizedUserName = "MOHAMEDABOSREA"

                        },
                        new ApplicationUser()
                        {
                            firstname = "ibrahem",
                            lastname = "youssef",
                            UserName = "ebrahimyoussef",
                            Email = "ebrahim@gmail.com",
                            PasswordHash = "ibr123456",
                            LockoutEnabled = false,
                            FacultyId = 2,
                            NormalizedUserName = "EBRAHIMYOUSSEF"
                        },
                        new ApplicationUser()
                        {
                            firstname = "osama",
                            lastname = "sabry",
                            UserName = "osamasabry",
                            Email = "osama@gmail.com",
                            PasswordHash = "os123456",
                            LockoutEnabled = false,
                            FacultyId = 2,
                            NormalizedUserName = "OSAMASABRY"
                        }
                        );
                }

                //}
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
                            ApplicationUserId = "1739c778-755b-4a71-85ab-5b497afaced1",
                            ScheduleId = 4
                        },
                         new Course()
                         {
                             Name = "ComputerGraphics",
                             CreditHrs = 3,
                             ApplicationUserId = "fb7b7de7-bbb7-47d8-b4d9-8ee817dd004a",
                             ScheduleId = 1
                         },
                          new Course()
                          {
                              Name = "Informatics",
                              CreditHrs = 3,
                              ApplicationUserId = "1739c778-755b-4a71-85ab-5b497afaced1",
                              ScheduleId = 2
                          },
                           new Course()
                           {
                               Name = "Network",
                               CreditHrs = 4,
                               ApplicationUserId = "fb7b7de7-bbb7-47d8-b4d9-8ee817dd004a",
                               ScheduleId = 3
                           }
                        );

                }
                if (!context.Questions.Any())
                {
                    context.Questions.AddRange(
                        new Question()
                        {
                            Questionx = "The World Wide Web was not viable to the general public until ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "1980", B = "1990", C = "1995", D = "1993" }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "In the 1980s, only large universities and ________ were able to access the Internet including e - mail.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "news organizations", B = "TV stations", C = "the U.S. government", D = "corporations" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "A printer is classified as a(n) ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "integral part of every computer", B = " processing device", C = "output device", D = "system device" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "Which item below is not part of the computer IPOS cycle?",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "Information", B = "Processing", C = "Output", D = "Storage" }),
                            Hint = null,
                            Goal = "A",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = " ________ software controls all devices and operations completed by a computer.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "Major", B = "Application", C = "Program", D = "System" }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = " A series of steps that describe what a computer must do to solve a problem or perform a task is a(n) ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = " system procedure", B = "function", C = "scenario", D = "algorithm" }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "Which of the below is NOT a(n) output device?",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "Speaker", B = "Monitor", C = "Microphone", D = "Printer" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = " If you assign different user names for each user of your computer, the operating system creates a special ________ for each user.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "network", B = "disk drive", C = "profile", D = "email account" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "Windows and Macs are equipped with ________ capabilities to allow different devices to automatically be recognized by the system.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "plug-and-play", B = "library modules", C = "special nodes", D = "device recognition" }),
                            Hint = null,
                            Goal = "A",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "When one task uses its ________ or is interrupted by a task of higher priority, the task is suspended and the other task starts.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "CPA", B = "RAM", C = "buffer", D = "time slice" }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "To ensure that programs run quickly, operating systems use the computer's RAM as a(n) ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "accelerator", B = "command enhancer", C = "buffer", D = "quick access system" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "Starting more than one application at a time is known as ________",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "multitasking", B = "convergence", C = "loading foreground", D = "workload increase" }),
                            Hint = null,
                            Goal = "A",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "Program instructions and data are divided into fixed sized units called ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "blocks", B = "blocks", C = "pages", D = "packets" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "Press [Alt]+[Ctrl]+[Delete] to access the Windows ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "processor", B = "Task Manager", C = "shut-down sequence", D = "print function" }),
                            Hint = null,
                            Goal = "B",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "When Task Manager is visible, select the ________ tab to view a list of processes running.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "Processes", B = "Procedures", C = "Programs", D = "People" }),
                            Hint = null,
                            Goal = "A",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "________ is a feature that enables subtle animations and translucent glass windows that can be custom colored.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "Aero", B = "Peek", C = "Shake", D = "Snap" }),
                            Hint = null,
                            Goal = "A",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "The status bar of the Task Manager provides information about the ________ and memory usage.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "disk drive", B = "input/output", C = "CPU", D = "printer" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "To enable a printer to keep up with the amount of data sent to it by the computer, a ________ program is used.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "print accelerator", B = "multitasking", C = "switching", D = "spooling" }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = " A type of computer operating system user interface that is commonly used today is ________",
                            Answer = JsonSerializer.Serialize(new Answer() { A = " command line", B = "voice command", C = "graphical (GUI)", D = "menu driven" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "The lower edge of the PC desktop screen displays a ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "menu bar", B = "status bar", C = "command bar", D = "taskbar" }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = @"In a command line operating system, the command copy C:\myfile.txt F:\myfile.txt will copy ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = " myfile from disk C to disk F", B = "myfile from disk F to disk C", C = " everything on disk C to disk F", D = "Nothing" }),
                            Hint = null,
                            Goal = "A",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "Information stored in a computer's RAM is ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "permanent", B = "non-volatile", C = "temporary", D = "slower than secondary memory" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = " An operating system available at no cost is the ________ operating system.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "Snow Leopard", B = "Windows", C = "Linux", D = "Unix" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "The original Macintosh operating system called Mac OS was released in:",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "1974", B = "1980", C = "1984", D = "1985" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "The Linux operating system called ________ was released to promote One Laptop per Child.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "All-in-one", B = "Linux Devine", C = "Sugar", D = "Free for all" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "Windows dominates the market for operating systems at about ________ percent.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "65", B = "85", C = "75", D = "90" }),
                            Hint = null,
                            Goal = "B",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = " A(n) ________ operating system works in conjunction with other networked computers.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "server", B = "embedded", C = "standalone", D = "specialized" }),
                            Hint = null,
                            Goal = "A",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "Google's operating system that supports many types of mobile devices is called ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "Astrid", B = "Google Mobile", C = "CDMA", D = "Android" }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = " ________ are designed to copy files periodically in case your hard drive fails.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "Disk copy programs", B = "Backup software utilities", C = "Disk manager programs", D = "Selective copy programs" }),
                            Hint = null,
                            Goal = "B",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = " A ________ can find bad sectors on your hard drive and block access to them.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "disk scanning program", B = "disk cleanup utility", C = "search utility", D = "repair program" }),
                            Hint = null,
                            Goal = "A",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "One of the most important utility software packages for any computer connected to the Internet is ________",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "backup software", B = "antivirus software", C = "an Internet browser", D = "accessories software" }),
                            Hint = null,
                            Goal = "B",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = " A ________ system utility helps you keep your hard drive organized and efficient.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "disk organizer", B = "disk indexer", C = " system manager", D = "file manager" }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "Over time, your disk drive tends to develop small areas scattered across its surfaces. The way to fix this problem is to use a ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "reorganizer", B = "disk defragmentation utility", C = "disk segment cleaner", D = "disk copier utility" }),
                            Hint = null,
                            Goal = "B",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "One method of reducing the space a file takes up on your disk is called file ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "archive", B = "shorten", C = "compression", D = "extract" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "A utility that is handy for people with poor eyesight is called a(n) ________ utility.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "enlarger", B = "magnifier", C = "binocular", D = "enhancer" }),
                            Hint = null,
                            Goal = "B",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "One step you should take to keep your operating system current is to enable ________.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "troubleshooting", B = "an uninterruptable power supply", C = "automatic system updates", D = "an off-site storage area" }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "One method to help troubleshoot a start-up problem is to start Windows in ________ mode.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "basic", B = "console", C = "beginners", D = "safe" }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "When your computer is configured to recognize ________ commands even handicapped people may use it.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "typed", B = "mouse", C = "written", D = "voice" }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 2,
                            Difficulty = "Moderate"
                        },
                        new Question()
                        {
                            Questionx = "A(n) ________ is a set of instructions that a computer uses to accomplish a task, such as word processing.",
                            Answer = JsonSerializer.Serialize(new Answer() { A = "icon", B = "command", C = "file", D = "program" }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },

                        new Question()
                        {
                            Questionx = "A(n) ________ is a collection of information that is stored on a computer under a single name.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "document",
                                B = "catalog",
                                C = "file",
                                D = "section"
                            }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "Every file is stored in a(n) ________.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "icon",
                                B = "command",
                                C = "file",
                                D = "program"
                            }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "Depending on whether you are working on your own computer, in a computer in a lab or library,you may have to ________ before you can use the computer.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "Answer some questions",
                                B = "Get permission",
                                C = "Submit a request",
                                D = "Log on"
                            }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "If there are two or more user ________ on the computer start-up screen, click on your own and sign in. ",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "Areas",
                                B = "Workspaces",
                                C = "Names",
                                D = "Icons"
                            }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "Depending on where you are working, there are often different ________ procedures. ",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "logon",
                                B = "password",
                                C = "saving",
                                D = "operating"
                            }),
                            Hint = null,
                            Goal = "A",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "The ________ may contain the Start Button and one or more applications buttons.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "taskbar",
                                B = "command line",
                                C = "notification area",
                                D = "program buttons"
                            }),
                            Hint = null,
                            Goal = "A",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "The Recycle Bin icon is an icon you can click to see what you have marked for ________.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "saving",
                                B = "cleaning",
                                C = "deletion",
                                D = "printing"
                            }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "The right side of the Taskbar is known as the ________ area and is sometimes referred to as the system tray.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "active program",
                                B = "notification",
                                C = "trouble",
                                D = "message"
                            }),
                            Hint = null,
                            Goal = "B",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = " The screen that appears after Windows starts up and you sign on is known as the ________.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "application indicator",
                                B = "desktop",
                                C = "work zone",
                                D = "program Initiator"
                            }),
                            Hint = null,
                            Goal = "B",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "The ________ is sometimes also called wallpaper.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "background",
                                B = "backdrop",
                                C = "desktop background",
                                D = "program area"
                            }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "The ________ is any symbol that displays on your screen in response to moving your mouse.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "Start button",
                                B = "taskbar",
                                C = "scroll bar",
                                D = "mouse pointer"
                            }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "When you insert a USB flash drive into one of the USB ports, the ________ program automatically runs.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "disk cleaner",
                                B = "Windows Explorer",
                                C = "USB reader",
                                D = "AutoPlay"
                            }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "Whenever the AutoPlay screen appears after inserting a USB flash drive, be sure to click the ________ on the AutoPlay screen to close AutoPlay.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "X",
                                B = "CD",
                                C = "music",
                                D = "open files"
                            }),
                            Hint = null,
                            Goal = "A",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "Point to the Start button to display the Start menu and then click ________ to gain access to the USB flash drive.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "My Documents",
                                B = "Network",
                                C = "Computer",
                                D = "My Pictures"
                            }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "After you click the Start Button, the ________ pane displays on the left of the screen.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "program list",
                                B = "information list",
                                C = "program screen",
                                D = "navigation screen"
                            }),
                            Hint = null,
                            Goal = "A",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "If you click Computer on the right of the Navigation Pane, a list of ________ devices contained within or attached to your computer will display.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "external",
                                B = "hardware",
                                C = "storage",
                                D = "software"
                            }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "A ________ is a collection of items, such as files and folders, assembled from various locations.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "drive",
                                B = "Pinned program",
                                C = "Location",
                                D = "Library"
                            }),
                            Hint = null,
                            Goal = "D",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                        new Question()
                        {
                            Questionx = "A row, column, or block of buttons or icons that allow you to perform commands with a single click is called a ________.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "command panel",
                                B = "toolbar",
                                C = "task bar",
                                D = "menu bar"
                            }),
                            Hint = null,
                            Goal = "B",
                            CourseId = 3,
                            Difficulty = "Hard"
                        },
                         new Question()
                         {
                             Questionx = "Once you have created a new folder you can type a new ________.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "owner",
                                 B = "folder type",
                                 C = "folder name",
                                 D = "length"
                             }),
                             Hint = null,
                             Goal = "C",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "In the address bar, the ________ which is the sequence of folders that lead to the current file or folder location is indicated.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "file",
                                 B = "path",
                                 C = "address",
                                 D = "description"
                             }),
                             Hint = null,
                             Goal = "B",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "The ________ is a program that captures an image of all or part of your computer screen.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "Screen Saver",
                                 B = "Copy Tool",
                                 C = "Extract Tool",
                                 D = "Snipping Tool"
                             }),
                             Hint = null,
                             Goal = "D",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "A(n) ________ displays when the contents of a window are not completely visible",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "search box",
                                 B = "scroll bar",
                                 C = "snip",
                                 D = "arrow"
                             }),
                             Hint = null,
                             Goal = "B",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "You can use a(n) ________ instead of a space between the words to facilitate sending your files over the Internet.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "percent symbol",
                                 B = "hyphen",
                                 C = "plus symbo",
                                 D = "underscore"
                             }),
                             Hint = null,
                             Goal = "D",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "To go to a particular Web site on the Internet you must type the ________ in the Internet address bar.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "route",
                                 B = "connection locator",
                                 C = "URL (Universal Resource Locator)",
                                 D = "site name"
                             }),
                             Hint = null,
                             Goal = "C",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "A(n) ________ file identifies a folder that contains one or more files that have been compressed to reduce their size.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "ZIP",
                                 B = "downsized",
                                 C = "reduced",
                                 D = "special"
                             }),
                             Hint = null,
                             Goal = "A",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "Zip files must be ________ before they can be used.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "opened in Microsoft Word",
                                 B = "opened in Internet Explorer",
                                 C = "extracted",
                                 D = "started"
                             }),
                             Hint = null,
                             Goal = "C",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = " If you want to save a file with a different name, click the File box in the upper left corner of the screen,and then click the ________ dialog box option.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "Rename",
                                 B = "New Name",
                                 C = "Save",
                                 D = "Save As"
                             }),
                             Hint = null,
                             Goal = "D",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "A(n) ________ is the fundamental unit of storage that enables Windows 7 to distinguish one set of information from another.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "glossary",
                                 B = "file",
                                 C = "document",
                                 D = "entry"
                             }),
                             Hint = null,
                             Goal = "B",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "Files are kept in ________ to help organize them into categories.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "containers",
                                 B = "pockets",
                                 C = "folders",
                                 D = "batches"
                             }),
                             Hint = null,
                             Goal = "C",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "A ________ provides a single access point from which you can open folders and files from different locations.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "folder window",
                                 B = "directory",
                                 C = "repository",
                                 D = "library"
                             }),
                             Hint = null,
                             Goal = "D",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "In the ________ under Computer, click your USB drive one time to display its contents in the File List.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "Start menu",
                                 B = "File button box",
                                 C = "navigation pane",
                                 D = " program list"
                             }),
                             Hint = null,
                             Goal = "A",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "On the task bar at the bottom of the desktop screen, click the ________ button to display the Libraries window.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "Windows Explorer",
                                 B = "Libraries",
                                 C = "Display",
                                 D = "Folder2"
                             }),
                             Hint = null,
                             Goal = "A",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "The ________ button opens an additional pane on the right side of the file list to display the contents of a file you select.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "Show File",
                                 B = "List",
                                 C = "Preview Pane",
                                 D = "Preview Pane"
                             }),
                             Hint = null,
                             Goal = "C",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "________ is a program that comes with Windows 7 with which you can create and edit drawings and display and edit stored photos.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "Image",
                                 B = "Paint",
                                 C = "Photo",
                                 D = "Edit"
                             }),
                             Hint = null,
                             Goal = "B",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },



                         new Question()
                         {
                             Questionx = "It is possible to have ________ window(s) open at one time.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "two",
                                 B = "only one",
                                 C = "multiple",
                                 D = "a maximum of three"
                             }),
                             Hint = null,
                             Goal = "C",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "To copy a file from one folder to another, first locate the folder and file you want to copy, click ________,and then click the folder where you want the copy to be located.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "Go",
                                 B = "Start",
                                 C = "Send to",
                                 D = "Apply"
                             }),
                             Hint = null,
                             Goal = "C",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "________ refers to the actions you perform to locate a command, folder, or a file.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "Referencing",
                                 B = "Referencing",
                                 C = "Browsing",
                                 D = "Investigating"
                             }),
                             Hint = null,
                             Goal = "B",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "The ________ button displays a list that is limited to the current session; thus, only locations you have accessed since starting Windows Explorer display on the list.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "Most Used Pages",
                                 B = "Favorite Pages",
                                 C = "Current Pages",
                                 D = "Recent Pages"
                             }),
                             Hint = null,
                             Goal = "D",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                         new Question()
                         {
                             Questionx = "________ is an area of the Start menu that displays all the available programs on your computer system.",
                             Answer = JsonSerializer.Serialize(new Answer()
                             {
                                 A = "All Programs",
                                 B = "My Programs",
                                 C = "Programs",
                                 D = "Installed Programs"
                             }),
                             Hint = null,
                             Goal = "A",
                             CourseId = 3,
                             Difficulty = "Moderate"
                         },
                        new Question()
                        {
                            Questionx = " In Office 2010 programs, the ________ displays in different shapes depending on the task you are performing and the area of the screen to which you are pointing.",
                            Answer = JsonSerializer.Serialize(new Answer()
                            {
                                A = "cursor",
                                B = "insertion point",
                                C = "mouse pointer",
                                D = "task bar"
                            }),
                            Hint = null,
                            Goal = "C",
                            CourseId = 3,
                            Difficulty = "Moderate"
                        }


                        );
                }
                //                //addQuestionToCourse(context);

                context.SaveChanges();

            }

        }
    }
}

    //public void addQuestionsIbrahiem()
    //{
    //    if (!context.Questions.Any())
    //    {
    //        context.Questions.AddRange(


    //            new Question()
    //            {
    //                Question1 = "A printer is classified as a(n) ________.",
    //                Answer = "{'A':' integral part of every computer','B':'processing device','C':' output device','D':'system device'}",
    //                Hint = null,
    //                Goal = "C",
    //                CourseId = 2,
    //                Difficulty = "Hard"
    //            },
    //            new Question()
    //            {
    //                Question1 = "Which item below is not part of the computer IPOS cycle?",
    //                Answer = "{'A':'Information','B':'Processing','C':'Output','D':'Storage'}",
    //                Hint = null,
    //                Goal = "A",
    //                CourseId = 2,
    //                Difficulty = "Moderate"
    //            },
    //            new Question()
    //            {
    //                Question1 = "________ software controls all devices and operations completed by a computer.",
    //                Answer = "{'A':'Major','B':'Application','C':'Program','D':'System'}",
    //                Hint = null,
    //                Goal = "D",
    //                CourseId = 2,
    //                Difficulty = "Moderate"
    //            },
    //            new Question()
    //            {
    //                Question1 = " A series of steps that describe what a computer must do to solve a problem or perform a task is a(n) ________.",
    //                Answer = "{'A':'system procedure','B':'function','C':'scenario','D':'algorithm'}",
    //                Hint = null,
    //                Goal = "D",
    //                CourseId = 2,
    //                Difficulty = "Moderate"
    //            },
    //            new Question()
    //            {
    //                Question1 = "Which of the below is NOT a(n) output device?",
    //                Answer = "{'A':'Speaker','B':'Monitor','C':'Microphone','D':'Printer'}",
    //                Hint = null,
    //                Goal = "C",
    //                CourseId = 2,
    //                Difficulty = "Moderate"
    //            },
    //            new Question()
    //            {
    //                Question1 = "Information stored in a computer's RAM is ________.",
    //                Answer = "{'A':'permanent','B':'non-volatile','C':'temporary','D':'slower than secondary memory'}",
    //                Hint = null,
    //                Goal = "C",
    //                CourseId = 2,
    //                Difficulty = "Hard"
    //            },
    //            new Question()
    //            {
    //                Question1 = "If you assign different user names for each user of your computer, the operating system creates a special ________ for each user.",
    //                Answer = "{'A':'network','B':'disk drive','C':'profile','D':'email account'}",
    //                Hint = null,
    //                Goal = "C",
    //                CourseId = 2,
    //                Difficulty = "Moderate"
    //            },
    //            new Question()
    //            {
    //                Question1 = "Windows and Macs are equipped with ________ capabilities to allow different devices to automatically be recognized by the system.",
    //                Answer = "{'A':'plug-and-play','B':' library modules','C':'special nodes','D':'device recognition'}",
    //                Hint = null,
    //                Goal = "A",
    //                CourseId = 2,
    //                Difficulty = "Moderate"
    //            },
    //            new Question()
    //            {
    //                Question1 = "When one task uses its ________ or is interrupted by a task of higher priority, the task is suspended and the other task starts.",
    //                Answer = "{'A':'CPA','B':'RAM','C':'buffer','D':'time slice'}",
    //                Hint = null,
    //                Goal = "D",
    //                CourseId = 2,
    //                Difficulty = "Moderate"
    //            },
    //            new Question()
    //            {
    //                Question1 = "To ensure that programs run quickly, operating systems use the computer's RAM as a(n) ________.",
    //                Answer = "{'A':'accelerator','B':'command enhancer','C':'buffer','D':'quick access system'}",
    //                Hint = null,
    //                Goal = "C",
    //                CourseId = 2,
    //                Difficulty = "Moderate"
    //            },
    //            new Question()
    //            {
    //                Question1 = "Starting more than one application at a time is known as ________.",
    //                Answer = "{'A':'multitasking','B':'convergence','C':'loading foreground','D':'workload increase'}",
    //                Hint = null,
    //                Goal = "A",
    //                CourseId = 2,
    //                Difficulty = "Moderate"
    //            },
    //            new Question()
    //            {
    //                Question1 = "Program instructions and data are divided into fixed sized units called ________.",
    //                Answer = { "A":"blocks","B":"batches","C":"pages","D":"packets"},
    //                        Hint = null,
    //                        Goal = "C",
    //                        CourseId = 2,
    //                        Difficulty = "Moderate"
    //                    },
    //                    new Question()
    //                    {
    //                        Question1 = "Press [Alt]+[Ctrl]+[Delete] to access the Windows ________.",
    //                        Answer = "{'A':'processor','B':' Task Manager','C':'shut-down sequence','D':'print function'}",
    //                        Hint = null,
    //                        Goal = "B",
    //                        CourseId = 2,
    //                        Difficulty = "Moderate"
    //                    },
    //                    new Question()
    //                    {
    //                        Question1 = "When Task Manager is visible, select the ________ tab to view a list of processes running.",
    //                        Answer = "{'A':'Processes','B':'Procedures','C':'Programs','D':'People'}",
    //                        Hint = null,
    //                        Goal = "A",
    //                        CourseId = 2,
    //                        Difficulty = "Moderate"
    //                    },
    //                    new Question()
    //                    {
    //                        Question1 = "The status bar of the Task Manager provides information about the ________ and memory usage.",
    //                        Answer = "{'A':'disk drive','B':'input/output','C':'CPU','D':'printer'}",
    //                        Hint = null,
    //                        Goal = "C",
    //                        CourseId = 2,
    //                        Difficulty = "Moderate"
    //                    },
    //                    new Question()
    //                    {
    //                        Question1 = "To enable a printer to keep up with the amount of data sent to it by the computer, a ________ program is used.",
    //                        Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
    //                        Hint = null,
    //                        Goal = "D",
    //                        CourseId = 2,
    //                        Difficulty = "Moderate"
    //                    },
    //                    new Question()
    //                    {
    //                        Question1 = "A type of computer operating system user interface that is commonly used today is ________.",
    //                        Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
    //                        Hint = null,
    //                        Goal = "D",
    //                        CourseId = 2,
    //                        Difficulty = "Moderate"
    //                    },
    //                    new Question()
    //                    {
    //                        Question1 = "The lower edge of the PC desktop screen displays a ________.",
    //                        Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
    //                        Hint = null,
    //                        Goal = "D",
    //                        CourseId = 2,
    //                        Difficulty = "Moderate"
    //                    },
    //                    new Question()
    //                    {
    //                        Question1 = "In a command line operating system, the command copy C:\myfile.txt F:\myfile.txt will copy ________.",
    //                        Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
    //                        Hint = null,
    //                        Goal = "A",
    //                        CourseId = 2,
    //                        Difficulty = "Hard"
    //                    },
    //                    new Question()
    //                    {
    //                        Question1 = " ________ is a feature that enables subtle animations and translucent glass windows that can be custom colored.",
    //                        Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
    //                        Hint = null,
    //                        Goal = "A",
    //                        CourseId = 2,
    //                        Difficulty = "Hard"
    //                    },
    //                    new Question()
    //                    {
    //                        Question1 = "An operating system available at no cost is the ________ operating system.",
    //                        Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
    //                        Hint = null,
    //                        Goal = "C",
    //                        CourseId = 2,
    //                        Difficulty = "Hard"
    //                    },
    //                    new Question()
    //                    {
    //                        Question1 = "The original Macintosh operating system called Mac OS was released in:",
    //                        Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
    //                        Hint = null,
    //                        Goal = "C",
    //                        CourseId = 2,
    //                        Difficulty = "Hard"
    //                    },
    //                    new Question()
    //                    {
    //                        Question1 = " The Linux operating system called ________ was released to promote One Laptop per Child.",
    //                        Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
    //                        Hint = null,
    //                        Goal = "C",
    //                        CourseId = 2,
    //                        Difficulty = "Hard"
    //                    },
    //                    new Question()
    //                    {
    //                        Question1 = " Windows dominates the market for operating systems at about ________ percent.",
    //                        Answer = "{'A':'blocks','B':'batches','C':'pages','D':'packets'}",
    //                        Hint = null,
    //                        Goal = "B",
    //                        CourseId = 2,
    //                        Difficulty = "Hard"
    //                    },




//            //  );
//        }
//        private static void addQuestionToCourse(dynamic context)
//        {


//        }
//    }
//}



