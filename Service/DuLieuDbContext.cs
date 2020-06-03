using Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;

namespace Service
{
    public class DuLieuDbContext : DbContext
    {
         public DbSet<DuLieuDuDoan> DuDoanList { get; set; }
         public DbSet<DuLieuLichSu> LichSuList { get; set; }

         public System.Data.Entity.DbSet<Entity.Models.Account> AccountList { get; set; }
    }

    public static class AccountDAO
    {
        private static string MaHoaMatKhau(String password)
        {
            //Tạo MD5 
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            String sb = "";
            for (int i = 0; i < hash.Length; i++)
            {
                sb += hash[i].ToString("x");
            }
            return sb;
        }

        private static string RandomSaltHash()
        {
            string rs = "";
            Random rd = new Random();
            for (int i = 0; i < 20; i++)
            {
                rs += Convert.ToString((Char)rd.Next(65, 90));
            }
            return rs;
        }
        static DuLieuDbContext db = new DuLieuDbContext();
        public static Account CheckLogin(string username, string password)
        {
            try
            {
                var rs = db.AccountList.SingleOrDefault(x => x.Username == username);

                if (rs != null && rs.Password == MaHoaMatKhau(rs.SaltPassword + password))
                {
                    return rs;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
        public static void AddAccount(Account acc)
        {
            acc.SaltPassword = RandomSaltHash();
            // ma hoa mat khau
            acc.Password = MaHoaMatKhau(acc.SaltPassword + acc.Password);
            db.AccountList.Add(acc);
            db.SaveChanges();
        }
        public static bool CheckUsername(string username)
        {
            foreach (Account c in db.AccountList)
            {
                if (c.Username == username)
                {
                    return false;
                }
            }
            return true;
        }

    }
    public static class DuLieuDuDoanDAO
    {
        static DuLieuDbContext db = new DuLieuDbContext();
        public static object GetDataFollowTime(DateTime dt)
        {
            var tempdb = (from DuLieuDuDoan in db.DuDoanList
                          where DuLieuDuDoan.time_db >= dt
                          orderby DuLieuDuDoan.time_db ascending
                          select new { Date = DuLieuDuDoan.time_db, Capacity = DuLieuDuDoan.capacity_db }).ToList();
            var tempdb1 = tempdb.Select(s => new { Date = s.Date.ToString("yyyy-MM-ddTHH:mm:ss"), Capacity = s.Capacity }).ToList();
            return tempdb1;
        }
        public static object GetDataSoSanhFollowTime(DateTime dtls)
        {
            var dataTb1 = db.LichSuList.AsNoTracking().ToList();
            var dataTb2 = db.DuDoanList.AsNoTracking().ToList();
            var tempss = (from DuLieuDuDoan in dataTb2
                          join DuLieuLichSu in dataTb1 on DuLieuDuDoan.time_db equals DuLieuLichSu.time into sosanh
                          from DuLieu in sosanh.DefaultIfEmpty()
                          where DuLieuDuDoan.time_db >= dtls
                          orderby DuLieuDuDoan.time_db ascending
                          select new
                          {
                              Date = DuLieuDuDoan.time_db,
                              CapacityTT = (DuLieu == null ? 191100 : DuLieu.capacity),
                              CapacityDB = DuLieuDuDoan.capacity_db,
                              GhiTT = (DuLieu == null ? 191100 : DuLieu.ghi),
                              GhiDB = DuLieuDuDoan.ghi_db
                          }).ToList();
            var tempdb1 = tempss.Select(s => new
            {
                Date = s.Date.ToString("yyyy-MM-ddTHH:mm:ss"),
                CapacityTT = s.CapacityTT,
                CapacityDB = (s.CapacityDB < 0 ? 0 : s.CapacityDB),
                GhiTT = s.GhiTT,
                GhiDB = s.GhiDB
            }).ToList();
            return tempdb1;
        }
    }

}