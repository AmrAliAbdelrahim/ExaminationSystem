using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    internal class FinalExam : Exam
    {
        public FinalExam(int _time, int _numberOfQuestions) : base(_time, _numberOfQuestions)
        {
        }


        public override void CreateListOfQuestions()
        {
            Questions = new Question[NumberOfQuestions];
            for (int i = 0; i < Questions?.Length; i++)
            {
                int choice;
                do
                {
                    Console.WriteLine("Please Enter Type Of Question (1 for MCQ | 2 For True | False)");
                } while (!(int.TryParse(Console.ReadLine() , out choice) && (choice is 1 or 2)));

                Console.Clear();

                if(choice == 1)
                {
                    Questions[i] = new McqQuestion();
                    Questions[i].AddQuestion();
                }
                else
                {
                    Questions[i] = new TFQuestion();
                    Questions[i].AddQuestion();
                }
            }
        }

        public override void ShowExam()
        {
            foreach(var question in Questions)
            {
                Console.WriteLine(question);

                for (int i = 0;i<question?.Answers?.Length;i++)
                    Console.WriteLine(question.Answers[i]);

                int userAnswerId;

                if(question?.GetType() == typeof(McqQuestion))
                {
                    do
                    {
                        Console.WriteLine("Please Enter The Answer Id");
                    } while (!(int.TryParse(Console.ReadLine() , out userAnswerId) && (userAnswerId is 1 or 2 or 3)));
                }
                else
                {
                    do
                    {
                        Console.WriteLine("Please Enter The Answer Id (1 For True | 2 For False)");
                    } while (!(int.TryParse(Console.ReadLine(), out userAnswerId) && (userAnswerId is 1 or 2)));
                }

                question.UserAnswer.Id = userAnswerId;
                question.UserAnswer.Text = question.Answers[userAnswerId-1].Text;

            }

            Console.Clear();

            // Calculate Marks

            int grade = 0, totalMarks = 0;

            for (int i = 0; i< Questions?.Length;i++)
            {
                totalMarks += Questions[i].Mark;

                if (Questions[i].UserAnswer.Id == Questions[i].RightAnswer.Id)
                {
                    grade += Questions[i].Mark;
                }

                Console.WriteLine($"Question {i+1} : {Questions[i].Body}");
                Console.WriteLine($"Your Answer => {Questions[i].UserAnswer.Text}");
                Console.WriteLine($"Right Answer => {Questions[i].RightAnswer.Text}");
            }

            Console.WriteLine($"Your Grade is {grade} from {totalMarks}");
        }
    }
}
