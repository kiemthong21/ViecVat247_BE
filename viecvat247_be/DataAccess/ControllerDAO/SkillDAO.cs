using BusinessObject;
using BussinessObject.Models;
using BussinessObject.Viecvat247Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.ControllerDAO
{
    public class SkillDAO
    {
        //Add new skill to database
        public static Skill AddSkill(Skill skill)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Skills.Add(skill);
                    context.SaveChanges();
                    return skill;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Skill> GetAllSkillsByJobId(int jid)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    List<Skill> skills = context.Skills
                    .Include(s => s.SkillCategory)
                    .Where(s => s.JobsSkills.Any(js => js.JobId == jid) && s.Status == 1)
                    .ToList();
                    return skills;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        //Update skill
        public static void UpdateSkill(Skill skill)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Entry<Skill>(skill).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Update skill
        public static void UpdateSkillCategory(SkillCategory cate)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Entry<SkillCategory>(cate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        //Get All Skill category
        public static List<SkillCategory> GetAllSkillCategory()
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var cate = context.SkillCategories.OrderBy(x => x.SkillCategoryName).ToList();
                    return cate;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static PaginatedList<SkillCategory> GetAllSkillCategory(string searchValue, int pageIndex, int pageSize, string orderBy)
        {
            var skillcategory = new List<SkillCategory>();
            int count = 0;
            IQueryable<SkillCategory> query = null;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.SkillCategories;

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.SkillCategoryName.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.SkillCategoryId);
                                break;
                            case "name":
                                query = query.OrderBy(e => e.SkillCategoryName);
                                break;
                            case "name desc":
                                query = query.OrderByDescending(e => e.SkillCategoryName);
                                break;
                            default:
                                query = query.OrderBy(e => e.SkillCategoryId);
                                break;
                        }
                    }
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    skillcategory = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<SkillCategory>(skillcategory, count, pageIndex, pageSize);
        }

        public static PaginatedList<SkillCategory> GetAllSkillCategory(string searchValue, string orderBy)
        {
            var skillcategory = new List<SkillCategory>();
            int count = 0;
            IQueryable<SkillCategory> query = null;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.SkillCategories;

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.SkillCategoryName.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.SkillCategoryId);
                                break;
                            case "name":
                                query = query.OrderBy(e => e.SkillCategoryName);
                                break;
                            case "name desc":
                                query = query.OrderByDescending(e => e.SkillCategoryName);
                                break;
                            default:
                                query = query.OrderBy(e => e.SkillCategoryId);
                                break;
                        }
                    }
                    count = query.Count();
                    skillcategory = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<SkillCategory>(skillcategory, count, 1, 999999);
        }

        //Add new skill category to database
        public static SkillCategory AddSkillCategory(SkillCategory cate)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.SkillCategories.Add(cate);
                    context.SaveChanges();
                    return cate;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Get all skill
        public static List<Skill> GetAllSkills()
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var skills = context.Skills.Include(x => x.SkillCategory).OrderBy(x => x.SkillName).ToList();
                    return skills;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static PaginatedList<Skill> GetAllSkills(string searchValue, string skillCate, int pageIndex, int pageSize, string orderBy)
        {
            var skill = new List<Skill>();
            int count = 0;
            IQueryable<Skill> query = null;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Skills.Include(c => c.SkillCategory);

                    if (!string.IsNullOrWhiteSpace(skillCate))
                    {
                        query = query.Where(e => e.SkillCategoryId!.ToString()!.Equals(skillCate));
                    }

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.SkillName!.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.SkillId);
                                break;
                            case "name":
                                query = query.OrderBy(e => e.SkillName);
                                break;
                            case "name desc":
                                query = query.OrderByDescending(e => e.SkillName);
                                break;
                            default:
                                query = query.OrderBy(e => e.SkillId);
                                break;
                        }
                    }
                    count = query.Count();
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    skill = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Skill>(skill, count, pageIndex, pageSize);
        }


        public static PaginatedList<Skill> GetAllSkills(string searchValue, string skillCate, string orderBy)
        {
            var skill = new List<Skill>();
            int count = 0;
            IQueryable<Skill> query = null;

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Skills.Include(c => c.SkillCategory);

                    if (!string.IsNullOrWhiteSpace(skillCate))
                    {
                        query = query.Where(e => e.SkillCategoryId!.ToString()!.Equals(skillCate));
                    }

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        query = query.Where(e => e.SkillName!.Contains(searchValue));
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "id desc":
                                query = query.OrderByDescending(e => e.SkillId);
                                break;
                            case "name":
                                query = query.OrderBy(e => e.SkillName);
                                break;
                            case "name desc":
                                query = query.OrderByDescending(e => e.SkillName);
                                break;
                            default:
                                query = query.OrderBy(e => e.SkillId);
                                break;
                        }
                    }
                    count = query.Count();
                    skill = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Skill>(skill, count, 1, 999999);
        }

        //Delete Skill
        public static void DeleteSkill(Skill skill)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var sk = context.Skills.SingleOrDefault(x => x.SkillId == skill.SkillId);
                    if (sk != null)
                    {
                        context.Skills.Remove(sk);
                        context.SaveChanges();
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Delete skill category
        public static void DeleteSkillCategory(SkillCategory cate)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var sk = context.SkillCategories.Include(x => x.Skills).SingleOrDefault(x => x.SkillCategoryId == cate.SkillCategoryId);
                    if (sk != null)
                    {
                        context.SkillCategories.Remove(sk);
                        foreach (var item in sk.Skills)
                        {
                            context.Skills.Remove(item);
                        }
                        context.SaveChanges();
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Get skill detail
        public static Skill GetSkillById(int id)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var skills = context.Skills.Include(x => x.SkillCategory).SingleOrDefault(x => x.SkillId == id);
                    return skills;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Skill GetSkillByName(string name)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var skills = context.Skills.Include(x => x.SkillCategory).SingleOrDefault(x => x.SkillName.Equals(name));
                    return skills;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static SkillCategory GetSkillCategoryByName(string name)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var skills = context.SkillCategories.SingleOrDefault(x => x.SkillCategoryName.Equals(name));
                    return skills;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Get skill category detail
        public static SkillCategory GetSkillCategoryById(int id)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    var skills = context.SkillCategories.SingleOrDefault(x => x.SkillCategoryId == id);
                    return skills;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
