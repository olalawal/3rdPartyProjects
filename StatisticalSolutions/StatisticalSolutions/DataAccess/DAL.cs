using Domain.StatisticalSolutions.Domain.Models.Context;
using StatisticalSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using StatisticalSolutions.Util;



namespace StatisticalSolutions.DataAccess
{
    public class DAL
    {

        
        /// <summary>
        /// method to check is student is registered for seminar
        /// </summary>
        /// <param name="model"></param>
        /// <param name="seminarid"></param>
        /// <returns></returns>
        internal bool CheckIfStudentIsRegistereredForSeminar(student model, int seminarid)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                student student = db.students.FirstOrDefault(s => s.Email == model.Email);
                if(student==null)
                {
                    return false;
                }
                else
                {
                    seminar seminar = db.seminars.FirstOrDefault(s => s.seminar_id==seminarid);
                   
                    // Check if user already exists
                    if (seminar.registrations.Any(z => z.student_id == model.student_id ))
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
               
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
               // db.Dispose();
            }
        }


        /// <summary>
        /// client model comes from chtml page or controller page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal int addclient(client model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {

                //first check if the client name is already in use
                client client = db.clients.FirstOrDefault(u => u.Name == model.Name && u.Email==model.Email);

                // Check if user already exists
                if (client == null)
                {
                    model.IsActive = true;
                    //DO whatever work is required to check etc
                    db.clients.Add(model);
                    db.SaveChanges();
                    return model.client_id;
                }
                else
                {
                    throw new CustomException("CLIENT_ALLREADY_EXIST");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               // db.Dispose();
            }
        }


        /// <summary>
        /// add student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal int addstudent(student model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                //first check if the client name is already in use
                student student = db.students.FirstOrDefault(u => u.Email == model.Email);

                // Check if user already exists
                if (student == null)
                {
                    model.IsActive = true;
                    //DO whatever work is required to check etc
                    db.students.Add(model);
                    db.SaveChanges();
                    return model.student_id;
                }
                else
                {
                    throw new CustomException("STUDENT_ALLREADY_EXIST");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               // db.Dispose();
            }
        }


        /// <summary>
        /// update student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal int updatestudent(student model) 
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                student st = db.students.FirstOrDefault(s => s.student_id == model.student_id);

                // Check if student already exists
                if (st != null)
                {
                    //DO whatever work is required to check etc
                    
                    st.FirstName = model.FirstName;                    
                    st.LastName = model.LastName;                    
                    st.Address1 = model.Address1;                    
                    st.Address2 = model.Address2;                   
                    st.City = model.City;                   
                    st.StateProvince = model.StateProvince;                   
                    st.Country = model.Country;                    
                    st.Phone = model.Phone;                    
                    st.ZipPostalCode = model.ZipPostalCode;                    
                    st.Fax = model.Fax;                    
                    st.BankAccountNumber = model.BankAccountNumber;                    
                    st.IsActive = model.IsActive;
                    st.Description = model.Description;
                  
                    db.SaveChanges();
                    return model.student_id;
                }
                else
                {
                    throw new CustomException("STUDENT_DOES_NOT_EXIST");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }


        /// <summary>
        /// delete student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal void deletestudent(int student_id) 
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                student student = db.students.FirstOrDefault(s => s.student_id == student_id);

                // Check if user already exists
                if (student != null)
                {
                    student.IsActive = false;
                    db.SaveChanges();                  
                }
                else
                {
                    throw new CustomException("STUDENT_DOES_NOT_EXIST");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }

       /// <summary>
       /// update client
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        internal int updateclient(client model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {             
                client client = db.clients.FirstOrDefault(c => c.client_id == model.client_id);
                // Check if user already exists
                if (client != null)
                {
                    client.Name = model.Name;
                    client.Address1 = model.Address1;
                    client.Address2 = model.Address2;
                    client.City = model.City;
                    client.StateProvince = model.StateProvince;
                    client.Country = model.Country;
                    client.Phone = model.Phone;
                    client.ZipPostalCode = model.ZipPostalCode;
                    client.Fax = model.Fax;
                    client.Description = model.Description;
                    client.IsActive = model.IsActive;
                  
                    db.SaveChanges();
                    return model.client_id;
                }
                else
                {
                    throw new CustomException("CLIENT_DOES_NOT_EXIST");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }


        /// <summary>
        /// delete client
        /// </summary>
        /// <param name="client_id"></param>
        internal void deleteclient(int client_id) 
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                client client = db.clients.FirstOrDefault(s => s.client_id == client_id);

                // Check if user already exists
                if (client != null)
                {
                    client.IsActive = false;
                    db.SaveChanges();
                }
                else
                {
                    throw new CustomException("CLIENT_DOES_NOT_EXIST");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }


        /// <summary>
        /// update seminar
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal int updateseminar(seminar model) 
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                seminar objseminar = db.seminars.FirstOrDefault(s => s.seminar_id == model.seminar_id);

                // Check if user already exists
                if (objseminar != null)
                {
                    objseminar.instructor_id = model.instructor_id;
                    objseminar.TitleHtml = model.TitleHtml;
                    objseminar.EventDetailsHtml = model.EventDetailsHtml;
                    objseminar.Description = model.Description;                    
                    objseminar.StartDate = model.StartDate;
                    objseminar.Enddate = model.Enddate;                    
                    objseminar.Address1 = model.Address1;
                    objseminar.Address2 = model.Address2;
                    objseminar.City = model.City;
                    objseminar.StateProvince = model.StateProvince;
                    objseminar.Country = model.Country;
                    objseminar.Phone = model.Phone;
                    objseminar.Email = model.Email;
                    objseminar.ZipPostalCode = model.ZipPostalCode;
                    objseminar.Fax = model.Fax;
                    objseminar.ContactEmail = model.ContactEmail;
                    objseminar.ContactPhone = model.ContactPhone;
                    objseminar.ContactWebsite = model.ContactWebsite;
                    objseminar.IsActive = model.IsActive;
                    db.SaveChanges();
                    return model.seminar_id;
                }
                else
                {
                    throw new CustomException("SEMINAR_DOES_NOT_EXIST");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }


        /// <summary>
        /// delete seminar
        /// </summary>
        /// <param name="seminar_id"></param>
        internal void deleteseminar(int seminar_id)  
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                seminar seminar = db.seminars.FirstOrDefault(s => s.seminar_id == seminar_id); 

                // Check if user already exists
                if (seminar != null)
                {
                    seminar.IsActive = false;
                    seminar.instructor_id = null;
                    db.SaveChanges();
                }
                else
                {
                    throw new CustomException("SEMINAR_DOES_NOT_EXIST");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }


        /// <summary>
        /// update instructor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal int updateInstructor(instructor model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {

                instructor objinstructor = db.instructors.FirstOrDefault(ins => ins.instructor_id == model.instructor_id);

                // Check if instructor already exists
                if (objinstructor != null)
                {
                    objinstructor.Name = model.Name;                   
                    objinstructor.Description = model.Description;                  
                    objinstructor.Address1 = model.Address1;
                    objinstructor.Address2 = model.Address2;
                    objinstructor.City = model.City;
                    objinstructor.StateProvince = model.StateProvince;
                    objinstructor.Country = model.Country;
                    objinstructor.Phone = model.Phone;
                    objinstructor.Email = model.Email;
                    objinstructor.ZipPostalCode = model.ZipPostalCode;
                    objinstructor.Fax = model.Fax;
                    objinstructor.DetailsHtml = model.DetailsHtml;
                    objinstructor.ImageName = model.ImageName;
                    objinstructor.ImagePath = model.ImagePath;        
                    objinstructor.IsActive = model.IsActive;
                    db.SaveChanges();
                    return model.instructor_id;
                }
                else
                {
                    throw new CustomException("INSTRUCTOR_DOES_NOT_EXIST");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }


        /// <summary>
        /// delete seminar
        /// </summary>
        /// <param name="seminar_id"></param>
        internal void deleteInstructor(int instructor_id)  
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                instructor instructor = db.instructors.FirstOrDefault(ins => ins.instructor_id == instructor_id);

                // Check if instructor already exists
                if (instructor != null)
                {
                    instructor.IsActive = false;
                    db.SaveChanges();
                }
                else
                {
                    throw new CustomException("INSTRUCTOR_DOES_NOT_EXIST");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }


        /// <summary>
        /// client model comes from chtml page or controller page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal int registerforseminarbystudentandseminarid( registration model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                if (model != null)
                {
                     //first check if the client name is already in use
                    client client=new client();
                    DateTime currentDatetime = DateTime.Now;
                    model.student.IsActive = true;
                    
                 
                    if (model.client == null || !string.IsNullOrEmpty(model.client.Name))
                        client = db.clients.FirstOrDefault(c => c.Name == model.client.Name);

                    if (client != null)
                        model.client_id = client.client_id;

                    //set client to null of model to prevent duplicate insertion                   
                    model.client = null;

              
                    //check user is registered for for seminar
                  if(db.registrations.Any(z => z.student_id == model.student_id && z.client_id == model.client_id && z.seminar_id == model.seminar_id))
                  {
                      throw new CustomException("STUDENT_ALLREADY_REGISTERED");
                  }

                  //model.seminar = null;
                //DO whatever work is required to check etc                   
                model.Registerdate = currentDatetime;
             
                db.registrations.Add(model);
                db.SaveChanges();
              
                }
                return model.id;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               // db.Dispose();
            }
        }

        /// <summary>
        /// seminar model comes from chtml page or controller page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal int addseminar(seminar model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {                
                // Check if user already exists and is not alrady registered
                if (model != null)
                {
                   if( string.IsNullOrEmpty(model.Description))
                       throw new CustomException("SEMINAR_DESCRIPTION_IS_NULL");

                      if( string.IsNullOrEmpty(model.TitleHtml))
                       throw new CustomException("SEMINAR_TITLEHTML_IS_NULL");

                     
                    //TO do add more valiadtion i.e start date end date etc, location
                      seminar seminar = db.seminars.FirstOrDefault(s => s.TitleHtml == model.TitleHtml && s.StartDate == model.StartDate && s.Enddate == model.Enddate
                        && s.Address1 == model.Address1 && s.Address2 == model.Address2 && s.City == model.City && s.StateProvince == model.StateProvince && s.Country == model.Country);
                    
                    

                    if (seminar==null)
                    {
                        model.IsActive = true;
                        //DO whatever work is required to check etc
                        db.seminars.Add(model);                       
                        db.SaveChanges();
                        return model.seminar_id;                       
                    }
                    else
                    {
                        throw new CustomException("SEMINAR_ALLREADY_EXIST");
                    }
                }
                else
                {
                    throw new CustomException("SEMINAR_MODEL_SUPPLIED_IS_NULL");
                }
             
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               // db.Dispose();
            }
        }


        /// <summary>
        /// seminar model comes from chtml page or controller page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal int addInstructor(instructor model) 
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                // Check if user already exists and is not alrady registered
                if (model != null)
                {
                    if (string.IsNullOrEmpty(model.Email))
                        throw new CustomException("INSTRUCTOR_EMAIL_IS_NULL");

                    if (string.IsNullOrEmpty(model.Name))
                        throw new CustomException("INSTRUCTOR_NAME_IS_NULL");

                    //TO do add more valiadtion i.e start date end date etc, location
                    instructor instructor = db.instructors.FirstOrDefault(ins => ins.Name == model.Name && ins.Address1 == model.Address1 && ins.Email == model.Email 
                        && ins.City == model.City && ins.StateProvince == model.StateProvince && ins.Country == model.Country);

                    if (instructor == null) 
                    {                        
                        //DO whatever work is required to check etc
                        db.instructors.Add(model);
                        db.SaveChanges();
                        return model.instructor_id;
                    }
                    else
                    {
                        throw new CustomException("INSTRUCTOR_ALLREADY_EXIST");
                    }
                }
                else
                {
                    throw new CustomException("INSTRUCTOR_MODEL_SUPPLIED_IS_NULL");
                }

            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }

       /// <summary>
       /// add contact message
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        internal int addcontactmessage(message model)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                // Check if user already exists
                if (model != null)
                {
                    if(string.IsNullOrEmpty(model.Body))
                        throw new CustomException("MESSAGE_BODY_IS_NULL");

                      if(string.IsNullOrEmpty(model.Email))
                          throw new CustomException("MESSAGE_EMAIL_IS_NULL");

                    DateTime currentDate =  DateTime.Now;
                    model.MessageDate = currentDate;
                    //DO whatever work is required to check etc
                    db.messages.Add(model);
                    db.SaveChanges();
                    return model.id;
                }
                else
                {
                    throw new CustomException("MESSAGE_MODEL_SUPPLIED_IS_NULL");
                }
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               // db.Dispose();
            }
        }


        /// <summary>
        /// get seminar list
        /// </summary>
        /// <returns></returns>
        internal List<seminar> getseminars()
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                DateTime today = DateTime.Now;
                List<seminar> seminars = db.seminars.Where(s => s.IsActive && s.StartDate >= today).OrderBy(s => s.StartDate).OrderBy(s => s.Starttime).Distinct().ToList();
                return seminars;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               // db.Dispose();
            }
        }

        /// <summary>
        /// get seminar list
        /// </summary>
        /// <returns></returns>
        internal List<seminar> getfutureseminars()
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                DateTime today = DateTime.Now;
                List<seminar> seminars = db.seminars.Where(s => s.IsActive && s.StartDate >= today).OrderBy(s => s.StartDate).OrderBy(s => s.Starttime).Distinct().ToList();
                return seminars;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }


        /// <summary>
        /// get seminar by seminar id
        /// </summary>
        /// <param name="seminar_id"></param>
        /// <returns></returns>
        internal seminar getseminarbyid(int seminar_id)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                seminar seminar = db.seminars.FirstOrDefault(s => s.seminar_id == seminar_id);



                return seminar;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }

        /// <summary>
        /// get seminar id by name
        /// </summary>
        /// <param name="seminarTitle"></param>
        /// <returns></returns>
        internal int getseminaridbyname(string seminarTitle)
        {
            
            try
            {
                using(StatisticalSolutionsContext db = new StatisticalSolutionsContext())
                {
                    DateTime today = DateTime.Now;
                    int seminar_id = db.seminars.Where(s => s.TitleHtml.ToLower() == seminarTitle.ToLower() && s.StartDate >= today && s.IsActive).OrderBy(s => s.StartDate).OrderBy(s => s.Starttime).FirstOrDefault().seminar_id;
                    return seminar_id;
                }
               
            }
            catch (CustomException ex)
            { 
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get seminars by instructor id      
        /// </summary>
        /// <param name="seminarTitle"></param>
        /// <returns></returns>
        internal List<seminar> getseminarsbyinstructorid(int instructor_id) 
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                DateTime today = DateTime.Now;
                List<seminar> seminars = db.seminars.Where(s => s.instructor_id == instructor_id && s.StartDate >= today && s.IsActive).OrderBy(s => s.StartDate).OrderBy(s => s.Starttime).Distinct().ToList();
                return seminars;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }

       
        /// <summary>
        /// get seminar instructor list
        /// </summary>
        /// <returns></returns>
        internal List<seminar> getseminars(bool isActive)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                var semIns = db.seminars.Where(s => s.IsActive == isActive).OrderByDescending(s => s.StartDate).OrderByDescending(s => s.Endtime).Distinct().ToList();
                                        
                return semIns;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        } 

              
        /// <summary>
        /// get inactive consultants
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        internal List<instructor> getinstructors(bool isActive) 
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
               var instructors = db.instructors.Where(ins=>ins.IsActive==isActive).OrderBy(ins=>ins.Name).Distinct().ToList();
               return instructors;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }
              

        /// <summary>
        /// get seminat list by student id
        /// </summary>
        /// <param name="student_id"></param>
        /// <returns></returns>
        internal List<seminar> getseminarsbystudentid(int student_id) 
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                List<seminar> semimars = (from sem in db.seminars
                              join reg in db.registrations
                                on sem.seminar_id equals reg.seminar_id
                              where reg.student_id == student_id
                              select sem).Distinct().ToList();
                return semimars;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }


        /// <summary>
        /// get instructor list
        /// </summary>
        /// <returns></returns>
        internal List<instructor> getinstructors()
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                List<instructor> instructors = db.instructors.Where(s => s.IsActive).OrderBy(ins => ins.Name).Distinct().ToList();
                return instructors;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }


        /// <summary>
        /// get instructor by instructor id
        /// </summary>
        /// <param name="seminar_id"></param>
        /// <returns></returns>
        internal instructor getinstructorbyid(int instructor_id)  
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                instructor instructor = db.instructors.FirstOrDefault(ins => ins.instructor_id == instructor_id);
                return instructor;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }

        /// <summary>
        /// get student by student id
        /// </summary>
        /// <param name="student_id"></param>
        /// <returns></returns>
        internal student getstudentbyid(int student_id)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                student student = db.students.FirstOrDefault(s => s.student_id == student_id);
                return student;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               // db.Dispose();
            }
        }

        /// <summary>
        /// get client by client id 
        /// </summary>
        /// <param name="client_id"></param>
        /// <returns></returns>
        internal client getcompaniesbyid(int client_id)
        { 
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                client client = db.clients.FirstOrDefault(c => c.client_id == client_id);
                return client;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               // db.Dispose();
            }
        }
        

        /// <summary>
        /// get clients list
        /// </summary>
        /// <returns></returns>
        internal List<client> getCompanies()  
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                List<client> clients = db.clients.Where(c=>c.IsActive).OrderBy(c=>c.Name).Distinct().ToList();  
                return clients;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
              
                throw ex;
            }
            finally
            {
               // db.Dispose();
            }
        }

        
        /// <summary>
        /// get inactive clients list
        /// </summary>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        internal List<client> getCompanies(bool IsActive)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                List<client> clients = db.clients.Where(c => c.IsActive == IsActive).OrderBy(c => c.Name).Distinct().ToList();
                return clients;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }


        /// <summary>
        /// get country list
        /// </summary>
        /// <returns></returns>
        internal List<Countries> getCountries() 
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                List<Countries> countries = db.Countries.OrderBy(c=>c.Country).Distinct().ToList();
                return countries;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               // db.Dispose();
            }
        }

        /// <summary>
        /// get registered seminars
        /// </summary>
        /// <returns></returns>
        internal List<seminar> getregisteredseminars()
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                DateTime today = DateTime.Now;
                List<seminar> seminars = (from sem in db.seminars
                                             join reg in db.registrations on sem.seminar_id equals reg.seminar_id
                                             where sem.StartDate >= today && sem.IsActive
                                             orderby sem.TitleHtml
                                          select sem).Distinct().ToList();
                    return seminars;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }

        /// <summary>
        /// get filtered seminars 
        /// </summary>
        /// <param name="filterText"></param>
        /// <returns></returns>
        internal List<SeminarEntity> getfilterregisteredseminars(string filterText)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                Expression<Func<registration, bool>> predicate = PredicateBuilder.True<registration>();
                string searchText = filterText.Trim();
                switch (searchText.ToLower())
                {

                    
                    case "paid":                  
                       
                        predicate = (searchText == "" ? predicate : predicate.And(e => e.Paid == true));
                        break;              

                    case "unpaid":

                        predicate = (searchText == "" ? predicate : predicate.And(e => e.Paid != true));
                        break;
                    case "attended":

                        predicate = (searchText == "" ? predicate : predicate.And(e => e.Attendend == true));
                        break;

                    case "not attended":

                        predicate = (searchText == "" ? predicate : predicate.And(e => e.Attendend != true));
                        break;
                }

                IQueryable<registration> regPredicate = db.registrations.AsExpandable().Where(predicate);

                DateTime today=DateTime.Now;
                List<SeminarEntity> seminars = (from sem in db.seminars
                                          join reg in regPredicate on sem.seminar_id equals reg.seminar_id
                                          where sem.StartDate >= today && sem.IsActive
                                          orderby sem.TitleHtml
                                          select new SeminarEntity 
                                          { 
                                              seminar_id=sem.seminar_id,
                                              //instructor_id=sem.instructor_id,
                                              TitleHtml=sem.TitleHtml,
                                              //Description=sem.Description,
                                              //EventDetailsHtml=sem.EventDetailsHtml,
                                              //Address1=sem.Address1,
                                              //Address2=sem.Address2,
                                              //City=sem.City,
                                              //StateProvince=sem.StateProvince,                                                                                          
                                              //Country=sem.Country,
                                              //ZipPostalCode=sem.ZipPostalCode,
                                              //StartDate=sem.StartDate,
                                              //Starttime=sem.Starttime,
                                              //Enddate=sem.Enddate,
                                              //Endtime=sem.Endtime,
                                              //Email=sem.Email,
                                              //Phone=sem.Phone,
                                              //Fax=sem.Fax,
                                              //EarlyBirdPrice=sem.EarlyBirdPrice,
                                              //NormalPrice=sem.NormalPrice,
                                              //IsActive=sem.IsActive,
                                              //ContactPhone = sem.ContactPhone,
                                              //ContactEmail = sem.ContactEmail,
                                              //ContactWebsite = sem.ContactWebsite,
                                              
                                          
                                          }).Distinct().ToList();
                    return seminars;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }

        

        /// <summary>
        /// get student registered for a particular seminar
        /// </summary>
        /// <param name="seminar_id"></param>      
        /// <returns></returns>
        internal List<student> getseminarregisteredstudents(int seminar_id)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
               List<student> students = (from st in db.students 
                                        join reg in db.registrations on st.student_id equals reg.student_id
                                        orderby st.LastName
                                        where reg.seminar_id == seminar_id  && st.IsActive
                                        select st).Distinct().ToList();

                return students;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }

        //internal List<seminar> getfutureseminarsstartdate()  
        //{
        //    StatisticalSolutionsContext db = new StatisticalSolutionsContext();
        //    try
        //    {
        //       //getting all seminars now but will change code later for only future seminars
        //        DateTime today = DateTime.Now;
        //        List<seminar> seminars = db.seminars.Where(s => s.StartDate >= today && s.IsActive).OrderBy(sem =>sem.StartDate).OrderBy(sem => sem.Starttime).Distinct().ToList();
        //        return seminars; 
        //    }
        //    catch (CustomException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //       // db.Dispose();
        //    }
        //}

        ///// <summary>
        ///// get future seminar start dates
        ///// </summary>
        ///// <param name="seminar_id"></param>
        ///// <returns></returns>
        //internal List<seminar> getfutureseminarsstartdate(int seminar_id)
        //{
        //    StatisticalSolutionsContext db = new StatisticalSolutionsContext();
        //    try
        //    {
        //        DateTime today = DateTime.Now;
        //        //getting all seminars now but will change code later for only future seminars
        //        List<seminar> seminars = db.seminars.Where(s => s.seminar_id == seminar_id && s.StartDate >= today).OrderBy(sem => sem.StartDate).OrderBy(sem => sem.Starttime).Distinct().ToList(); 
        //        return seminars;
        //    }
        //    catch (CustomException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        // db.Dispose();
        //    }
        //}


        /// <summary>
        /// get students
        /// </summary>
        /// <returns></returns>
        internal List<student> getstudents() 
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                //getting all students
                List<student> students = db.students.Where(s => s.IsActive).OrderBy(s => s.LastName).Distinct().ToList();
                return students;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               // db.Dispose();
            }
        }

        /// <summary>
        /// get students by seminar id
        /// </summary>
        /// <returns></returns>
        internal List<student> getstudents(int seminar_id)
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                //getting all students
                List<student> students = (from s in db.students
                                         join reg in db.registrations on s.student_id equals reg.student_id
                                         where reg.seminar_id == seminar_id && s.IsActive
                                         orderby s.LastName
                                         select s).Distinct().ToList();
                return students;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }


        /// <summary>
        /// get action of inactive students
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        internal List<student> getinactivestudents(bool isActive) 
        {
            StatisticalSolutionsContext db = new StatisticalSolutionsContext();
            try
            {
                //getting all students
                List<student> students = db.students.Where(s => s.IsActive == isActive).OrderBy(s => s.LastName).Distinct().ToList();
                return students;
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // db.Dispose();
            }
        }

    }
}