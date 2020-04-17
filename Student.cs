using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lab2
{
    public class Student : IDisposable
    {
        #region fields

        private bool _Disposed = false;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DynamicArray<int> Scores { get; set; }

        #endregion fields

        #region contructors
        public Student(string lastName, string firstName, int numScores)
        {
            FirstName = firstName;
            LastName = lastName;
            Scores = new DynamicArray<int>(numScores);
        }
        #endregion constructors

        #region members
        public double GetAverageScore()
        {
            double sum = 0;
            double averageScore = 0;
            for (int i = 0; i < Scores.GetLength(); ++i)
            {
                sum += Scores[i]; 
            }
            if (Scores.GetLength() > 0)
            {
                averageScore = sum / Scores.GetLength();
            }
            return averageScore;
        }

        #endregion members

        #region IDisposable Support


        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    Scores?.Dispose();
                    Scores = null;
                }
                _Disposed = true;
            }
        }

         ~Student()
         {
            Console.WriteLine($"Finalizing Student from thread {Thread.CurrentThread.ManagedThreadId}");
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Console.WriteLine($"Disposing Student from thread {Thread.CurrentThread.ManagedThreadId}");
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region overrides

        public override string ToString()
        {
            return $"{LastName, -15} {FirstName, -15} {Scores.GetLength()} {GetAverageScore().ToString("0.000"), -3}";
        }

        #endregion overrides

    }
}
