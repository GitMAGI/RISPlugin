using GeneralPurposeLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DataAccessLayer.Mappers;

namespace DataAccessLayer
{
    public partial class RISDAL
    {        
        // NO ENTITYFRAMEWORK
        public IDAL.VO.EsameVO GetEsameById(string esamidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            IDAL.VO.EsameVO esam = null;
            try
            {
                string connectionString = this.CCConnectionString;

                long esamidid_ = long.Parse(esamidid);
                string table = this.EsameTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "esameidid",
                            Op = DBSQL.Op.Equal,
                            Value = esamidid_,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", LibString.ItemsNumber(data)));
                if (data != null)
                { 
                    if(data.Rows.Count == 1)
                    {
                        esam = EsameMapper.EsamMapper(data.Rows[0]);
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(esam), LibString.TypeName(esam)));
                    }
                }               
            }
            catch (Exception ex)
            {
                log.Info(string.Format("DBSQL Query Executed! Retrieved 0 record!"));
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return esam;
        }
        public List<IDAL.VO.EsameVO> GetEsamiByIds(List<string> esamidids)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IDAL.VO.EsameVO> esams = null;
            try
            {
                string connectionString = this.GRConnectionString;

                string table = this.EsameTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>();
                int i = 0;
                foreach (string esamidid in esamidids)
                {
                    long esamidid_ = long.Parse(esamidid);
                    DBSQL.QueryCondition tmp = new DBSQL.QueryCondition()
                    {
                        Key = "radioidid",
                        Op = DBSQL.Op.Equal,
                        Value = esamidid_,
                        Conj = i < esamidids.Count - 1 ? DBSQL.Conj.Or : DBSQL.Conj.None
                    };
                    conditions.Add("id" + i, tmp);
                    i++;
                }

                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", LibString.ItemsNumber(data)));
                if (data != null)
                {
                    if (data.Rows.Count == 1)
                    {
                        esams = EsameMapper.EsamMapper(data);
                        log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(esams), LibString.TypeName(esams)));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info(string.Format("DBSQL Query Executed! Retrieved 0 record!"));
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return esams;
        }
        public List<IDAL.VO.EsameVO> GetEsamiByRichiesta(string richidid)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            List<IDAL.VO.EsameVO> esams = null;
            try
            {
                string connectionString = this.GRConnectionString;

                long esamidid_ = long.Parse(richidid);
                string table = this.EsameTabName;

                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                {
                    {
                        "id",
                        new DBSQL.QueryCondition() {
                            Key = "esampres",
                            Op = DBSQL.Op.Equal,
                            Value = esamidid_,
                            Conj = DBSQL.Conj.None
                        }
                    }
                };
                DataTable data = DBSQL.SelectOperation(connectionString, table, conditions);
                log.Info(string.Format("DBSQL Query Executed! Retrieved {0} record!", LibString.ItemsNumber(data)));
                if (data != null)
                {
                    esams = EsameMapper.EsamMapper(data);
                    log.Info(string.Format("{0} Records mapped to {1}", LibString.ItemsNumber(esams), LibString.TypeName(esams)));
                }
            }
            catch (Exception ex)
            {
                log.Info(string.Format("DBSQL Query Executed! Retrieved 0 record!"));
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return esams;
        }
        public int SetEsame(IDAL.VO.EsameVO data)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.EsameTabName;

            try
            {
                string connectionString = this.GRConnectionString;
                string esamidid = data.radioidid.HasValue ? data.radioidid.Value.ToString() : null;
                List<string> autoincrement = new List<string>() { "radioidid" };

                if (esamidid == null)
                {
                    // INSERT NUOVA
                    result = DBSQL.InsertOperation(connectionString, table, data, autoincrement);
                    log.Info(string.Format("Inserted {0} new records!", result));
                }
                else
                {
                    long radioidid_ = long.Parse(esamidid);
                    // UPDATE
                    Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "radioidid",
                                Value = radioidid_,
                                Op = DBSQL.Op.Equal,
                                Conj = DBSQL.Conj.None,
                            }
                        },
                    };
                    result = DBSQL.UpdateOperation(connectionString, table, data, conditions, new List<string>() { "radioidid" });
                    log.Info(string.Format("Updated {0} records!", result));
                }
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return result;
        }
        public IDAL.VO.EsameVO NewEsame(IDAL.VO.EsameVO data)
        {
            IDAL.VO.EsameVO result = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.EsameTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                List<string> pk = new List<string>() { "RADIOIDID" };
                List<string> autoincrement = new List<string>() { "RaDiOIdiD" };
                // INSERT NUOVA
                DataTable res = DBSQL.InsertBackOperation(connectionString, table, data, pk, autoincrement);
                if (res != null)
                    if (res.Rows.Count > 0)
                    {
                        result = EsameMapper.EsamMapper(res.Rows[0]);
                        log.Info(string.Format("Inserted new record with ID: {0}!", result.radioidid));
                    }
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return result;
        }
        public List<IDAL.VO.EsameVO> NewEsami(List<IDAL.VO.EsameVO> data)
        {
            List<IDAL.VO.EsameVO> results = null;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.EsameTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                List<string> pk = new List<string>() { "RADIOIDID" };
                List<string> autoincrement = new List<string>() { "rAdIoIdiD" };
                // INSERT NUOVA
                DataTable res = DBSQL.MultiInsertBackOperation(connectionString, table, data, pk, autoincrement);
                if (res != null && res.Rows.Count > 0)
                {
                    results = EsameMapper.EsamMapper(res);
                }
                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        string tmp = "";
                        int o = 0;
                        foreach (IDAL.VO.EsameVO tmp_ in results)
                        {
                            tmp += tmp_.radioidid.Value.ToString();
                            if (o < results.Count - 1)
                                tmp += ", ";
                            o++;
                        }
                        log.Info(string.Format("Inserted {0} new records with IDs: {1}!", LibString.ItemsNumber(results), tmp));
                    }
                }
                else
                {
                    log.Info(string.Format("No records Inserted!"));
                }
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return results;
        }
        public int DeleteEsameById(string radioidid)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.EsameTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                long radioidid_ = long.Parse(radioidid);
                // UPDATE
                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "radioidid",
                                Value = radioidid_,
                                Op = DBSQL.Op.Equal,
                                Conj = DBSQL.Conj.None,
                            }
                        },
                    };
                result = DBSQL.DeleteOperation(connectionString, table, conditions);
                log.Info(string.Format("Deleted {0} records!", result));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return result;
        }
        public int DeleteEsameByRichiesta(string richidid)
        {
            int result = 0;

            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Starting ..."));

            string table = this.EsameTabName;

            try
            {
                string connectionString = this.GRConnectionString;

                long radiopres_ = long.Parse(richidid);
                // UPDATE
                Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>()
                    {
                        { "id",
                            new DBSQL.QueryCondition()
                            {
                                Key = "radiopres",
                                Value = radiopres_,
                                Op = DBSQL.Op.Equal,
                                Conj = DBSQL.Conj.None,
                            }
                        },
                    };
                result = DBSQL.DeleteOperation(connectionString, table, conditions);
                log.Info(string.Format("Deleted {0} records!", result));
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + "\n" + ex.Message);
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return result;
        }
    }
}