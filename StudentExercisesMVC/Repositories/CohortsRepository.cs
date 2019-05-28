using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using StudentExercisesMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Repositories
{
    public class CohortsRepository
    {
        private static IConfiguration _config;

        public static void SetConfig(IConfiguration configuration)
        {
            _config = configuration;
        }

        public static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public static List<Cohort> GetCohorts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT c.Id,
                                            c.Designation
                                        FROM Cohort c
                                    ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Cohort> cohorts = new List<Cohort>();
                    while (reader.Read())
                    {
                        Cohort cohort = new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Designation = reader.GetString(reader.GetOrdinal("Designation"))
                        };

                        cohorts.Add(cohort);
                    }

                    reader.Close();

                    return (cohorts);
                }
            }
        }

        public static Cohort GetCohort(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT c.Id,
                                               c.Designation
                                        FROM Cohort c
                                        WHERE c.Id = @CohortId
                                    ";
                    cmd.Parameters.Add(new SqlParameter("@CohortId", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    Cohort cohort = null;
                    if (reader.Read())
                    {
                        cohort = new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Designation = reader.GetString(reader.GetOrdinal("Designation")),
                        };
                    }

                    reader.Close();

                    return cohort;
                }
            }
        }

        public static Cohort CreateCohort(Cohort cohort)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Cohort (Designation)
                                        OUTPUT INSERTED.Id
                                        VALUES (@des)";
                    cmd.Parameters.Add(new SqlParameter("@des", cohort.Designation));

                    int newId = (int)cmd.ExecuteScalar();
                    return cohort;
                }
            }
        }

        public static Cohort EditCohort(int id, Cohort cohort)
        {

            // TODO: Add update logic here
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" UPDATE Cohort
                                             SET Designation = @des
                                             WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@des", cohort.Designation));
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return cohort;
        }

        public static void DeleteCohort(int id)
        {
            // TODO: Add delete logic here
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Cohort WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }
        }
    }
}


