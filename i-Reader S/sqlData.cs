using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;

namespace i_Reader_S
{
    public class SqlData
    {
        public  static ResourceManager Rm =new ResourceManager("i_Reader_S.Properties.Resources", Assembly.GetExecutingAssembly());
        public static readonly string ConStr = "server ="+Dns.GetHostName()+ @"\sqlexpress;database=i-Reader_S;integrated security = true; min pool size=1;max pool size=100;Connection Lifetime = 30; Enlist=true"; //ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
        public static readonly string ConStrMaster = ConStr.Replace("i-Reader_S", "master");

        //打印信息查询
        public static DataTable SelectPrintInfo(string createTime)
        {
            var strsql = new StringBuilder();
            strsql.Append("select b.SensorID,a.Sampleno,d.TestItemName, a.flag+case ");
            for (int i = -1; i > -15; i--)
            {
                strsql.Append(" when Result = ");
                strsql.Append(i);
                strsql.Append(" then '");
                strsql.Append(Rm.GetString("ResultError" + (-i).ToString()));
                strsql.Append("' ");
            }  
            strsql.Append(" when d.Accurancy=2 then CONVERT(varchar(20),coNVERT(decimal(18,2),Result) ) end,a.Unit,a.ODdata from ResultList a,ProductInfo b,CalibData c ,TestItemInfo d where a.TestItemID=d.TestItemID and b.ProductID=d.ProductID and a.CalibDataID=c.CalibDataid and b.ProductID = c.ProductID and a.Createtime='");
            strsql.Append(createTime);
            strsql.Append("'");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        public static DataTable SelectUnitRatio(string time)
        {
            var strsql = new StringBuilder();
            strsql.Append("select Ratio from ResultList a,CalibData b, ProductInfo c,TestItemInfo d where a.CalibDataID = b.CalibDataid and b.ProductID = c.ProductID and c.ProductID = d.ProductID and a.TestItemID = d.TestItemID and a.CreateTime = '");
            strsql.Append(time);
            strsql.Append("'");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //更新试剂仓的定标信息
        public static void UpdateReagentCalibData(string strCalibdataid, string reagentStrorId)
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "update ReagentStore set CalibDataID = ");
            strsql.Append(strCalibdataid);
            strsql.Append(" where ReagentStoreid=");
            strsql.Append(reagentStrorId);
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }

        public static void UpdateResult(DateTime time,string type)
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "update resultlist set result = result"+type+"0.01 where result>0 and createtime='");
            strsql.Append(time);
            strsql.Append("'");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }

        //用户信息验证
        public static DataTable DbCheckUserinfo(string username, string password)
        {
            var strsql = new StringBuilder();
            strsql.Append("select * from userinfo where username='");
            strsql.Append(username);
            strsql.Append("' and password='");
            strsql.Append(password);
            strsql.Append("'");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }
        //删除待测
        public static void DeleteFromWrokrunlist(string seq)
        {
            var strsql = new StringBuilder();
            strsql.Append("delete from workrunlist where sequence=");
            strsql.Append(seq);
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }
        //插入新项目
        public static void InsertNewProductInfo(string[] param)
        {
            var strsql = new StringBuilder();
            strsql.Append("insert into productinfo values(");
            strsql.Append(param[0]);
            strsql.Append(",'");
            strsql.Append(param[1]);
            strsql.Append("',");
            strsql.Append(param[2]);
            strsql.Append(",");
            strsql.Append(param[3]);
            strsql.Append(",");
            strsql.Append(param[4]);
            strsql.Append(",");
            strsql.Append(param[5]);
            strsql.Append(",");
            strsql.Append(param[6] );
            strsql.Append(")");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }

        //插入新项目
        public static void InsertNewTestItem(string[] param)
        {
            var strsql = new StringBuilder();
            strsql.Append("insert into testiteminfo values(");
            strsql.Append(param[0]);
            strsql.Append(",");
            strsql.Append(param[7]);
            strsql.Append(",'");
            strsql.Append(param[8]);
            strsql.Append("','");
            strsql.Append(param[9]);
            strsql.Append("','");
            strsql.Append(param[9]);
            strsql.Append("',1,");
            strsql.Append(param[10]);
            strsql.Append(",");
            strsql.Append(param[11]);
            strsql.Append(",");
            strsql.Append(param[12]);
            strsql.Append(",");
            strsql.Append(param[13]);
            strsql.Append(",");
            strsql.Append(param[14]);
            strsql.Append(",");
            strsql.Append(param[15]);
            strsql.Append(",'',");
            strsql.Append(param[16]);
            strsql.Append(")");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());

        }

        //将定标信息插入到定标记录表
        public static int InsertIntoCalibdata(string cpmc, string lot, DateTime dtPdate, DateTime epdate, string funtype,
            double[] param)
        {
            //如果存在同样记录，则不插入
            var calibDataExist = new StringBuilder();
            calibDataExist.Append("select calibdataid from Calibdata where productid=");
            var insertCalibData = new StringBuilder();
            insertCalibData.Append("insert into calibdata values(");
            //产品名称 productid
            insertCalibData.Append(cpmc);
            calibDataExist.Append(cpmc);
            calibDataExist.Append(" and lotno=");
            insertCalibData.Append(",");
            //批号lot
            insertCalibData.Append(lot);
            insertCalibData.Append(",'");
            calibDataExist.Append(lot);
            calibDataExist.Append(" and productdate='");
            //生产日期
            insertCalibData.Append(dtPdate.ToString("yyyy-MM-dd"));
            insertCalibData.Append("','");
            calibDataExist.Append(dtPdate);
            calibDataExist.Append("' and expiredate='");
            //过期日期
            insertCalibData.Append(epdate.ToString("yyyy-MM-dd"));
            insertCalibData.Append("',");
            calibDataExist.Append(epdate);
            calibDataExist.Append("' and formulatypeid=");
            //公式类型
            insertCalibData.Append(funtype);
            insertCalibData.Append(",");
            calibDataExist.Append(funtype);
            var str = "ABCDEFG";
            for (var i = 0; i < 7; i++)
            {
                calibDataExist.Append(" and formulaparam");
                calibDataExist.Append(str[i]);
                calibDataExist.Append("=");
                calibDataExist.Append(param[i]);
                insertCalibData.Append(param[i].ToString(CultureInfo.InvariantCulture));
                insertCalibData.Append(i < 6 ? "," : ")");
            }
            var dt =
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, calibDataExist.ToString())
                    .Tables[0];
            if (dt.Rows.Count > 0)
                return int.Parse(dt.Rows[0][0].ToString());
            return
                ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, insertCalibData.ToString()) >
                0
                    ? 0
                    : -1;
        }

        //插入新结果
        public static void InsertIntoNewResult(string sampleno, string createtime, string testitemid, string result,
            string unit, string csvdatapath, string imgdatapath, string tyfixstr, string calibdataid,
            string reagentstoreid, string turnplateid, string shelfid, string oddata,string flag)
        {
            var count = int.Parse(SelectResultCount("").Rows[0][0].ToString());
            if (count >= 60000)
            {
                var strsql1 = new StringBuilder();
                strsql1.Append("delete from ResultList where SampleNo  in (select top 1 SampleNo from ResultList order by SampleNo, CreateTime)and CreateTime in (select top 1 CreateTime from ResultList order by SampleNo, CreateTime)");
                ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql1.ToString());
            }
            var strsql = new StringBuilder();
            strsql.Append("insert into resultlist values('");
            strsql.Append(sampleno);
            strsql.Append("','");
            strsql.Append(createtime);
            strsql.Append("',");
            strsql.Append(testitemid);
            strsql.Append(",");
            strsql.Append(result);
            strsql.Append(",'");
            strsql.Append(unit);
            strsql.Append("','");
            strsql.Append(csvdatapath);
            strsql.Append("','");
            strsql.Append(imgdatapath);
            strsql.Append("','");
            strsql.Append(tyfixstr);
            strsql.Append("',");
            strsql.Append(calibdataid);
            strsql.Append(",");
            strsql.Append(reagentstoreid);
            strsql.Append(",");
            strsql.Append(turnplateid);
            strsql.Append(",");
            strsql.Append(shelfid);
            strsql.Append(",'");
            strsql.Append(oddata);
            strsql.Append("','");
            strsql.Append(flag);
            strsql.Append("')");
    
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }

        //插入新样本
        public static void InsertIntoRunlist(string sequence, string sampleNo, string testItemId, string createTime,
            string workingStatus, string tyFixDataId, string reagentStoreId, string dilutionRatio, string reactionTime,
            string calibDataId)
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "INSERT INTO WorkRunList(Sequence,SampleNo,TestItemID,CreateTime,WorkingStatus,TyFixStr,ReagentStoreID,DilutionRatio,ReactionTime,CalibDataID)VALUES(");
            strsql.Append(sequence);
            strsql.Append(",'");
            strsql.Append(sampleNo);
            strsql.Append("',");
            strsql.Append(testItemId);
            strsql.Append(",'");
            strsql.Append(createTime);
            strsql.Append("','");
            strsql.Append(workingStatus);
            strsql.Append("','");
            strsql.Append(tyFixDataId);
            strsql.Append("',");
            strsql.Append(reagentStoreId);
            strsql.Append(",");
            strsql.Append(dilutionRatio);
            strsql.Append(",");
            strsql.Append(reactionTime);
            strsql.Append(",");
            strsql.Append(calibDataId);
            strsql.Append(")");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }

        //插入质控点
        public static void InsertQcSetting(string productid, string qcSampleno, string qctargetValue, string qCsdvalue)
        {
            var strsql = new StringBuilder();
            strsql.Append("insert qcsetting values(");
            strsql.Append(productid);
            strsql.Append(",'");
            strsql.Append(qcSampleno);
            strsql.Append("',");
            strsql.Append(qctargetValue);
            strsql.Append(",");
            strsql.Append(qCsdvalue);
            strsql.Append(")");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }

        //查找定标参数
        public static DataTable SelectCalibData(int calibDataId)
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select formulatypeid,formulaparamA,formulaparamB,formulaparamC,formulaparamD,formulaparamE,formulaparamF,formulaparamG from calibdata where calibdataid=");
            strsql.Append(calibDataId);
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //根据卡仓号查询校准曲线id和批号
        public static DataTable SelectCalibDataIdLotNoByReagentId(string reagentid)
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select b.LotNo,a.CalibDataID from ReagentStore a,CalibData b where  a.CalibDataID=b.CalibDataid and ReagentStoreID=");
            strsql.Append(reagentid);
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //查看数据库是否存在
        //根据seq查找待测列表中的样本号，反应时间等
        public static DataTable SelectDbExist()
        {
            var strsql = new StringBuilder();
            strsql.Append("select name From sysdatabases where name = 'i-Reader_S' ");
            return
                ExecuteDataset(new SqlConnection(ConStrMaster), CommandType.Text, strsql.ToString())
                    .Tables[0
                    ];
        }

        //查询工作仓
        public static DataTable SelectDefaultReagentStoreId()
        {
            var strsql = new StringBuilder();
            strsql.Append("select reagentstoreid,reagentleft from ReagentStore  where ISWorkStore=1");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //刷新正在测试表
        public static DataTable SelectExceptionList()
        {
            var strsql = new StringBuilder();
            strsql.Append("select createtime, sampleno,c.TestItemName,a.flag+ case ");
            for (int i = -1; i > -15; i--)
            {
                strsql.Append(" when Result = ");
                strsql.Append(i);
                strsql.Append(" then '");
                strsql.Append(Rm.GetString("ResultError" + (-i).ToString()));
                strsql.Append("' ");
            }
            strsql.Append("  when Accurancy=2 then CONVERT(varchar(20),coNVERT(decimal(18,2),Result) )+a.unit   else  CONVERT(varchar(20), CONVERT(decimal(18,4),Result))+a.unit  end as result from ResultList a,CalibData b,TestItemInfo c where a.CalibDataID=b.CalibDataid and b.ProductID=c.ProductID and a.TestItemID=c.TestItemID and createtime>convert(char(10),getdate(),120) and createtime<convert(char(10),getdate()+1,120) and (result <0 or Result>c.WarningValue) order by createtime desc ");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        public static bool WarningValue(string createtime)
        {
            var strsql = new StringBuilder();
            strsql.Append("select * from ResultList a,CalibData b,TestItemInfo c where a.CalibDataID = b.CalibDataid and b.ProductID = c.ProductID and a.TestItemID = c.TestItemID and Result > c.WarningValue and createtime='");
            strsql.Append(createtime);
            strsql.Append("' ");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ].Rows.Count>0;
        }

        //查找矫正参数
        public static DataTable SelectFixData(int fixDataid)
        {
            var strsql = new StringBuilder();
            strsql.Append("select tyfixparama,tyfixparamb,tyfixparamc from tyfixdata where tyfixdataid = ");
            strsql.Append(fixDataid);
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //获取荧光计算参数
        public static DataTable SelectFluoCalMethod(string seq)
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select Method,FormulaParamA,FormulaParamB,FormulaParamC,FormulaParamD,FormulaParamE,FormulaParamF,FormulaParamG  from WorkRunList a,CalibData b,ProductInfo c where a.CalibDataID=b.CalibDataid and c.ProductID=b.ProductID and Sequence=");
            strsql.Append(seq);
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //选择最新的校准参数编号
        public static string SelectMaxFixId()
        {
            var strsql = new StringBuilder();
            strsql.Append("select MAX(TyFixDataID) from TyFixData");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ].Rows[0][0].ToString();
        }

        //查询产品id根据产品名称
        public static DataTable SelectProductIdByname(string productname)
        {
            var strsql = new StringBuilder();
            strsql.Append("select ProductID from productInfo where productName = '");
            strsql.Append(productname);
            strsql.Append("'");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //自动切换仓
        public static DataTable SelectproductidbyTestItemName(string testitem)
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select top 1 ReagentStoreID,b.productid from ReagentStore a,CalibData b,TestItemInfo c where a.CalibDataID=b.CalibDataid and b.ProductID=c.ProductID and c.TestItemName='");
            strsql.Append(testitem);
            strsql.Append("' and ReagentLeft>0 order by isworkstore desc, ExpireDate,LoadTime");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //查询产品id
        public static DataTable SelectproductidbyTestItemName1(string testitem)
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select productid from TestItemInfo where TestItemName='");
            strsql.Append(testitem);
            strsql.Append("'");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //查询产品id 测试项目id根据项目名
        public static DataTable SelectProductIdItemIdByname(string testitemname)
        {
            var strsql = new StringBuilder();
            strsql.Append("select ProductID, TestItemID from TestItemInfo where TestItemName = '");
            strsql.Append(testitemname);
            strsql.Append("'");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //查看qc历史统计
        public static DataTable SelectQcHistory()
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select count(*) as no,t1.sampleno,t1.year,t1.month,result from (select SampleNo,year(createtime) as year ,month(CreateTime) as month,day(CreateTime) as day,case when abs((avg(result)-QCTargetValue)/QCSDValue)>=3 then 3 when  abs((avg(result)-QCTargetValue)/QCSDValue)>=2 then 2 else 0 end as result from ResultList a , QCSetting b, CalibData c,TestItemInfo d where year(GETDATE())*12+month(getdate())-YEAR(CreateTime)*12-MONTH(CreateTime)<12 and right(d.TestItemName,2)='QC' and a.TestItemID=d.TestItemID and c.ProductID=d.ProductID and a.CalibDataID = c.CalibDataid and c.ProductID = b.ProductID and b.productid=0 and b.QCSampleNo = a.SampleNo group by SampleNo, year(CreateTime), MONTH(CreateTime), DAY(CreateTime), QCTargetValue, QCSDValue) t1 where result >0 group by result,year,month,sampleno order by year,month,sampleno");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        public static DataTable SelectQcResult(string month, string year, string productid)
        {
            var strsql = new StringBuilder();
            strsql.Append("select (result-target)/sd,qcsampleno,productname,day(testdate),result,target,sd from qcresult a,productinfo b  where a.productid=b.productid and a.productid=");
            strsql.Append(productid);
            strsql.Append(" and year(testdate)=");
            strsql.Append(year);
            strsql.Append(" and month(testdate)=");
            strsql.Append(month);
            strsql.Append(" order by qcsampleno,day(testdate)");
            return ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0];
        }

        //QC样本号查找
        public static DataTable SelectQcSampleno(string productname)
        {
            var strsql = new StringBuilder();
            strsql.Append("select qcsampleno from qcsetting where productid = ");
            strsql.Append(SelectProductIdByname(productname).Rows[0][0]);
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //查找qc设置
        public static DataTable SelectQcSetting(string productName)
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select a.productid, QCSampleNo as 样本号 ,QCtargetValue as 靶值,QCSDvalue as SD,productname from qcsetting a,ProductInfo b where  a.productid=b.ProductID and  b.ProductName = '");
            strsql.Append(productName);
            strsql.Append("'");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //查找有的QC项目
        public static DataTable SelectQcTestItem()
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select b.ProductName from TestItemInfo a, ProductInfo b where right(TestItemName, 2) = 'QC' and a.ProductID = b.ProductID");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //qc 最早最晚记录时间
        public static DataTable SelectQcTimeSpan()
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select YEAR( max(createtime)) as maxyear ,month( max(createtime)) as maxmonth , YEAR( min(createtime)) as minyear ,month( min(createtime)) as minmonth    from ResultList a , QCSetting b, CalibData c,TestItemInfo d  where right(d.TestItemName,2)='QC' and a.TestItemID=d.TestItemID  and c.ProductID=d.ProductID and a.CalibDataID = c.CalibDataid and c.ProductID =  b.ProductID and b.productid=0 and b.QCSampleNo = a.SampleNo and year(GETDATE())*12+month(getdate())-YEAR(CreateTime)*12-MONTH(CreateTime)<12 ");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //查询工作参数
        public static  DataTable SelectReactionParam(string seq)
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select b.DilutionRatioID,b.SensorID,b.ReactionTime,b.DropVolume from ReagentStore a, ProductInfo b,CalibData c where a.CalibDataID = c.CalibDataid and b.ProductID = c.ProductID and reagentstoreid=");
            strsql.Append(seq);
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //查询试剂仓信息
        public static  DataTable SelectReagentinfo()
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select a.ReagentStoreID,c.ProductName,b.LotNo,a.ReagentLeft,b.ExpireDate from ReagentStore a,CalibData b,ProductInfo c where b.ProductID = c.ProductID and a.CalibDataID = b.CalibDataid");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //结果数量
        public static  DataTable SelectResultCount(string searchconditon)
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select count(*) from ResultList a,CalibData b,TestItemInfo c where a.CalibDataID=b.CalibDataid and b.ProductID=c.ProductID and a.TestItemID=c.TestItemID ");
            strsql.Append(searchconditon);
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //插入结果
        public static  DataTable Selectresultinfo(string seq)
        {
            //图像，文件地址暂时不存  吸光度暂时不存  计算暂时不计算
            var strsql = new StringBuilder();
            strsql.Append(
                "select a.SampleNo,a.createtime,a.TestItemID,a.CalibDataID,a.ReagentStoreID,a.TurnPlateID,a.ShelfID,c.Unit,c.Ratio,c.UnitRatio,c.MinValue,c.MaxValue,a.TyFixStr,accurancy,c.TestItemName,c.productid  from WorkRunList a,CalibData b,TestItemInfo c where a.CalibDataID=b.CalibDataid and a.TestItemID = c.TestItemID and c.ProductID=b.ProductID and sequence =");
            strsql.Append(seq);
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //查询列表
        public static  DataTable SelectResultList(string page, string resultno, string searchcondition)
        {
            var headerstr = Rm.GetString("SearchResultHearder")?.Split('|');
            var strsql = new StringBuilder();
            strsql.Append("select sampleno as ");
            strsql.Append(headerstr?[0]);
            strsql.Append(",TestItemName as ");
            strsql.Append(headerstr?[1]);
            strsql.Append(",w.flag+ case  ");
            for (int i = -1; i > -15; i--)
            {
                strsql.Append(" when Result = ");
                strsql.Append(i);
                strsql.Append(" then '");
                strsql.Append(Rm.GetString("ResultError" + (-i).ToString()));
                strsql.Append("' ");
            }
            strsql.Append(" when Accurancy=2 then CONVERT(varchar(20),coNVERT(decimal(18,2),Result) )+w.unit else  CONVERT(varchar(20), CONVERT(decimal(18,4),Result))+w.unit  end as ");
            strsql.Append(headerstr?[2]);
            strsql.Append(",w.CreateTime as ");
            strsql.Append(headerstr?[3]);
            strsql.Append(" from resultlist w, (select top ");
            strsql.Append(resultno);
            strsql.Append(" CreateTime from  (select top ");
            strsql.Append(double.Parse(page) * double.Parse(resultno));
            strsql.Append(
                "  createtime from ResultList a,CalibData b,TestItemInfo c where a.CalibDataID=b.CalibDataid and b.ProductID=c.ProductID and a.TestItemID=c.TestItemID ");
            strsql.Append(searchcondition);
            strsql.Append(
                " order by CreateTime desc) w1 order by CreateTime) w2,CalibData w3,TestItemInfo w4 where w.CreateTime = w2.CreateTime and w.CalibDataID=w3.CalibDataid and w.TestItemID=w4.TestItemID and w3.ProductID=w4.ProductID order by w.CreateTime desc");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //刷新结果表
        //刷新正在测试表
        public static  DataTable SelectResultListToday()
        {
            //
            var strsql = new StringBuilder();
            strsql.Append("select createtime, sampleno,c.TestItemName,a.flag+ case ");
            for (int i = -1; i > -15; i--)
            {
                strsql.Append(" when Result = ");
                strsql.Append(i);
                strsql.Append(" then '");
                strsql.Append(Rm.GetString("ResultError" + (-i).ToString()));
                strsql.Append("' ");
            }
            strsql.Append(
                " when Accurancy=2 then CONVERT(varchar(20),coNVERT(decimal(18,2),Result) )+a.unit else  CONVERT(varchar(20), CONVERT(decimal(18,4),Result))+a.unit  end as result from ResultList a,CalibData b,TestItemInfo c where a.CalibDataID=b.CalibDataid and b.ProductID=c.ProductID and a.TestItemID=c.TestItemID and createtime>convert(char(10),getdate(),120) and createtime<convert(char(10),getdate()+1,120)  order by createtime desc ");

            return ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //根据seq查找待测列表中的样本号，反应时间等
        public static  DataTable SelectSampleNoTimeBySeq(string seq)
        {
            var strsql = new StringBuilder();
            strsql.Append("select sampleno,reactiontime from workrunlist where sequence = ");
            strsql.Append(seq);
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        
        //查找项目根据seq
        public static DataTable SelectProductIDbySeq(string seq)
        {
            var strsql=new StringBuilder();
            strsql.Append("select b.ProductID  from WorkRunList a,CalibData b where a.CalibDataID = b.CalibDataid and Sequence=");
            strsql.Append(seq);
            return
    ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
        ];
        }

        //查询可供选择的测试项目
        public static  DataTable SelectTestItem()
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select distinct ProductID from reagentStore a,CalibData b where a.CalibDataID=b.CalibDataid and reagentleft>0 ");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //项目查找
        public static  DataTable SelectTestIteminfo()
        {
            var strsql = new StringBuilder();
            strsql.Append(
                "select ProductID,TestItemID,TestItemName as 项目名,Unit as 单位,Ratio as 调整系数,ltrim( str(LowReferenceValue))+'-'+LTRIM( str(UpReferenceValue))  as 参考范围,Accurancy as 精确位数 ,UnitDefault,UnitRatio  from [i-Reader_S].[dbo].[TestItemInfo] where right(testitemname,2)<>'QC'");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString())
                    .Tables[0];
        }

        //根据产品id查询产品名称
        public static  string SelectTestItemNameById(string id)
        {
            var strsql = new StringBuilder();
            strsql.Append("select productname from productinfo where productid = ");
            strsql.Append(id);
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ].Rows[0][0].ToString();
        }

        //刷新正在测试表
        public static  DataTable SelectWorkRunlist()
        {
            var strsql = new StringBuilder();
            var hostQuery = ConfigurationManager.AppSettings["HostQuery"];
            strsql.Append(
                hostQuery == "0"
                    ? "select Sequence,SampleNo,b.TestItemName ,WorkingStatus from workrunlist a,TestItemInfo b,calibdata c where c.ProductID=b.ProductID and a.TestItemID=b.TestItemID and a.CalibDataID=c.CalibDataid"
                    : "select b,c,d,e from((select '0' as a, Sequence as b, SampleNo as c, b.TestItemName as d, WorkingStatus as e from workrunlist a, TestItemInfo b, calibdata c where c.ProductID = b.ProductID and a.TestItemID = b.TestItemID and a.CalibDataID = c.CalibDataid)union(select '1' as a, HostQueryList.[index] as b, SampleNo as c, TestItemName as d, '排队' as e from HostQueryList)) temp");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //更新产品项目信息
        public static void UpdateTestIteminfo(string testitemname, string unit, string unitratio, string ratio,
            string warningvalue,
            string accurancy, string productid, string testitemid)
        {
            var strsql = new StringBuilder();
            strsql.Append("update testiteminfo set testitemname='");
            strsql.Append(testitemname);
            strsql.Append("',unit='");
            strsql.Append(unit);
            strsql.Append("',unitratio=");
            strsql.Append(unitratio);
            strsql.Append(", ratio=");
            strsql.Append(ratio);
            strsql.Append(",warningvalue=");
            strsql.Append(warningvalue);
            strsql.Append(",accurancy=");
            strsql.Append(accurancy);
            strsql.Append(" where productid=");
            strsql.Append(productid);
            strsql.Append(" and testitemid=");
            strsql.Append(testitemid);
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }

        public static void UpdateDb(string strsql)
        {
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql);
        }

        //修改样本号
        public static void UpdateWorkRunlistSampleNobySeq(string seq, string sampleno)
        {
            var strsql = new StringBuilder();
            strsql.Append("update workrunlist set sampleno ='");
            strsql.Append(sampleno);
            strsql.Append("' where sequence =");
            strsql.Append(seq);
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }

        //删除待测样本
        public static void DeleteSampleNo(string seq)
        {
            var strsql = new StringBuilder();
            strsql.Append("delete from  workrunlist ");
            strsql.Append(" where sequence =");
            strsql.Append(seq);
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }
        //设置工作仓
        public static void SetDefaultReagentStoreId(string reagentStoreId)
        {
            var strsql = new StringBuilder();
            strsql.Append("update ReagentStore set ISWorkStore='false'");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
            var strsql1 = new StringBuilder();
            strsql1.Append("update ReagentStore set ISWorkStore='true' where reagentstoreid=");
            strsql1.Append(reagentStoreId);
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql1.ToString());
        }
        //根据seq更新工作状态
        public  static void UpdateWorkStatusBySeq(string status, string seq)
        {
            var strsql = new StringBuilder();
            strsql.Append("update workrunlist set workingstatus='");
            strsql.Append(status);
            strsql.Append("' where sequence = ");
            strsql.Append(seq);
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }
        //查找数据库是否存在
        public static string Dbok()
        {
            var strsql = new StringBuilder();
            strsql.Append("select COUNT(*) from sys.databases where name='i-Reader_S'");
            return ExecuteDataset(new SqlConnection(ConStrMaster), CommandType.Text, strsql.ToString()).Tables[0].Rows[0][0].ToString();
        }
        // 附加数据库
        public static bool Attachdb(string sMdbFile, string sLog)
        {
            if (SelectDbExist().Rows.Count > 0)
                return true;
            var strsql = new StringBuilder();
            strsql.Append("EXEC sp_attach_db   @dbname = '" + "i-Reader_S" + "',   @filename1   =   '");
            strsql.Append(sMdbFile);
            strsql.Append("',@filename2='");
            strsql.Append(sLog);
            strsql.Append("'");
            return
                ExecuteNonQuery(new SqlConnection(ConStrMaster), CommandType.Text, strsql.ToString()) >
                0;
        }
        //切换工作仓
        public static DataTable SwitchReagentStore()
        {
            var strsql = new StringBuilder();
            // strsql.Append(
              //"select top 1 reagentstoreid from ReagentStore a,CalibData b where a.CalibDataID=b.CalibDataid and a.ReagentLeft>0 and b.ProductID=(select d.ProductID from ReagentStore c,CalibData d where c.CalibDataID=d.CalibDataid and c.ISWorkStore=1) order by ExpireDate,LoadTime");
            strsql.Append(
                "select top 1 reagentstoreid from ReagentStore a,CalibData b where a.CalibDataID=b.CalibDataid and a.ReagentLeft>0 and b.ProductID=(select d.ProductID from ReagentStore c,CalibData d where c.CalibDataID=d.CalibDataid and c.ISWorkStore=1) order by LoadTime,ExpireDate");//2017-2-24
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0
                    ];
        }

        //修改qc设置
        public static  void UpdateQcSetting(string productid, string qcsamplenoold, string qcsamplenew, string targetnew,
            string sdnew)
        {
            var strsql = new StringBuilder();
            strsql.Append("update QCSetting set qcsampleno ='");
            strsql.Append(qcsamplenew);
            strsql.Append("',");
            strsql.Append("qctargetvalue=");
            strsql.Append(targetnew);
            strsql.Append(",qcsdvalue=");
            strsql.Append(sdnew);
            strsql.Append(",updatetime=getdate() where productid =");
            strsql.Append(productid);
            strsql.Append(" and qcsampleno='");
            strsql.Append(qcsamplenoold);
            strsql.Append("'");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }

        //反应中倒计时：
        public static  bool UpdateReactionTime()
        {
            var strsql = new StringBuilder();
            var str = Rm.GetString("I3111");
            strsql.Append(
                "update WorkRunList set WorkingStatus =replace('");
            strsql.Append(str);
            strsql.Append(":' +str(right(WorkingStatus,LEN(workingstatus)-");
            if (str != null) strsql.Append(str.Length + 1);
            strsql.Append(") -1),' ','') where WorkingStatus like '");
            strsql.Append(Rm.GetString("I3111"));
            strsql.Append("%' and workingstatus<>'反应中:0'");
            return ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()) > 0;
        }

        //更新试剂仓存量
        public static  void UpdateReagentLeft(string reagentid, int leftno)
        {
            var strsql = new StringBuilder();
            strsql.Append("update ReagentStore set ReagentLeft =");
            strsql.Append(leftno);
            strsql.Append(" where ReagentStoreID =");
            strsql.Append(reagentid);
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, strsql.ToString());
        }


        private static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            var cmd = new SqlCommand();
            bool mustCloseConnection;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            var retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText));
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            command.Connection = connection;
            command.CommandText = commandText;
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (commandParameters != null)
            {
                foreach (var p in commandParameters)
                {
                    if (p != null)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connection, commandType, commandText, null);
        }
        private static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            var cmd = new SqlCommand();
            bool mustCloseConnection;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            using (var da = new SqlDataAdapter(cmd))
            {
                var ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                if (mustCloseConnection)
                    connection.Close();
                return ds;
            }
        }

        //更新加载时间
        



        public static DataTable SelectTestItem(string productId)
        {
            var strsql = new StringBuilder();
            if (productId == "")
                strsql.Append("select TestItemName,a.productid from TestItemInfo a,ProductInfo b where a.ProductID=b.ProductID");
            else
            { 
            strsql.Append("select TestItemName,a.productid from TestItemInfo a,ProductInfo b where a.ProductID=b.ProductID and b.productid in(");
            strsql.Append(productId);
            strsql.Append(")  and testitemname <> 'HsCRP'");
            }
            return
            ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, strsql.ToString()).Tables[0];
        }

        internal static DataTable SelectSeq(string sampleNo)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select top 1 sequence,createtime from workrunlist where sampleNo='");
            str.Append(sampleNo);
            str.Append("' and workingstatus = 'ASU传送' order by createtime");
            return
    ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, str.ToString()).Tables[0
        ];
        }

        public static void UpdateResultYaji(string v, string unitratio, string str1)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update resultlist set flag = '*',result=result*");
            str.Append(str1);
            str.Append("/");
            str.Append(unitratio);
            str.Append(" where createtime='");
            str.Append(v);
            str.Append("'");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, str.ToString());
        }

        //更新加载时间
        public static void UpdateReagentLoadTime(string loadtime, string reagentstoreid)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update ReagentStore set LoadTime='");
            str.Append(loadtime);
            str.Append("' where ReagentStoreID='");
            str.Append(reagentstoreid);
            str.Append("'");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, str.ToString());
        }

        public static DataTable SelectQCResultCount(string sampleNo, string calibDataId, string createtime)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select *  from QCResult where sampleNo='");
            str.Append(sampleNo);
            str.Append("'  a.productid = b.productid and  createtime between '");
            str.Append(createtime);
            str.Append(" 00:00:00' and '");
            str.Append(createtime);
            str.Append(" 23;59:59' and sampleno='");
            str.Append(sampleNo);
            str.Append("'");
            return
    ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, str.ToString()).Tables[0
        ];
        }

        public static void InsertQCResult(string sampleNo, string productid, string createtime)
        {
            StringBuilder str2 = new StringBuilder();
            str2.Append("delete from QCResult where qcsampleno='");
            str2.Append(sampleNo);
            str2.Append("' and productid=");
            str2.Append(productid);
            str2.Append(" and testdate = '");
            str2.Append(createtime);
            str2.Append("'");

            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, str2.ToString());


            StringBuilder str = new StringBuilder();
            str.Append("select avg(Result),QCTargetValue,QCSDValue  from ResultList a,CalibData b,qcsetting c where a.CalibDataID=b.CalibDataid and b.ProductID=");
            str.Append(productid);
            str.Append("  and b.ProductID=c.ProductID and a.SampleNo=c.QCSampleNo and SampleNo='");
            str.Append(sampleNo);
            str.Append("' and createtime between '");
            str.Append(createtime);
            str.Append(" 00:00:00' and '");
            str.Append(createtime);
            str.Append(" 23:59:59' and result >0  group by QCTargetValue,QCSDValue");

            var result= ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, str.ToString()).Tables[0];


            StringBuilder str1 = new StringBuilder();
            str1.Append("insert into QCResult values(");
            str1.Append(productid);
            str1.Append(",'");
            str1.Append(sampleNo);
            str1.Append("','");
            str1.Append(createtime);
            str1.Append("',");
            str1.Append(result.Rows[0][0].ToString());
            str1.Append(",");
            str1.Append(result.Rows[0][1].ToString());
            str1.Append(",");
            str1.Append(result.Rows[0][2].ToString());
            str1.Append(",");
            str1.Append(((double.Parse(result.Rows[0][0].ToString())- double.Parse(result.Rows[0][1].ToString()))/ double.Parse(result.Rows[0][2].ToString())).ToString("F2"));
            str1.Append(")");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, str1.ToString());

        }

        public static DataTable  UpdateQCSettingValue(int year, int month)
        {
            var str = new StringBuilder();
            str.Append("select b.ProductID,QCSampleNo,STDEV(result),AVG(result) from ResultList a,QCSetting b,CalibData c where a.CalibDataID=c.CalibDataid and b.ProductID=c.ProductID and a.SampleNo = b.QCSampleNo and YEAR(CreateTime) = ");
            str.Append(year);
            str.Append(" and MONTH(CreateTime) = ");
            str.Append(month);
            str.Append("and Result > 0  and UpdateTime < '");
            str.Append(year);
            str.Append("-");
            str.Append(month);
            str.Append("-01' group by b.ProductID, QCSampleNo");
            return
    ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, str.ToString()).Tables[0
        ];
        }
        //2017-2-23
        //查询异常结果的照片文件名
        public static DataTable SelectPicNum(string creatrtime)
        {
            var str = new StringBuilder();
            str.Append("select ImgDataPath from Resultlist where createtime = '");
            str.Append(creatrtime);
            str.Append("'");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, str.ToString()).Tables[0];
        }
        //2017-2-24
        //查询所有用户

        public static DataTable SelectallUserName()
        {
            var str = new StringBuilder();
            str.Append("select * from UserInfo");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, str.ToString()).Tables[0];
        }

        //删除用户
        public static void DeleteUser(string UserName)
        {
            StringBuilder str = new StringBuilder();
            str.Append("Delete from UserInfo where username = '");
            str.Append(UserName);
            str.Append("'");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, str.ToString());
        }

        //寻找用户
        public static DataTable SelectOneUserName(string UserName)
        {
            var str = new StringBuilder();
            str.Append("select * from UserInfo where username = '");
            str.Append(UserName);
            str.Append("'");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, str.ToString()).Tables[0];
        }
        //添加新用户
        public static void InsertNewUser(string UserName, string Password, string Usertype)
        {
            StringBuilder str1 = new StringBuilder();
            str1.Append("INSERT INTO [i-Reader_S].[dbo].[UserInfo]([UserName],[Password] ,[UserType]) VALUES ('");
            str1.Append(UserName);
            str1.Append("', '");
            str1.Append(Password);
            str1.Append("','");
            str1.Append(Usertype);
            str1.Append("')");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, str1.ToString());
        }
        //对比用户名
        public static DataTable SelectUserName(string UserName)
        {
            var str = new StringBuilder();
            str.Append("select username from UserInfo where username = '");
            str.Append(UserName);
            str.Append("' ");
            return
                ExecuteDataset(new SqlConnection(ConStr), CommandType.Text, str.ToString()).Tables[0];
        }
        //更改用户密码
        public static void ChangePassword(string Username, string Password)
        {
            StringBuilder str = new StringBuilder();
            str.Append("Update UserInfo Set password = '");
            str.Append(Password);
            str.Append("' where username = '");
            str.Append(Username);
            str.Append("'");
            ExecuteNonQuery(new SqlConnection(ConStr), CommandType.Text, str.ToString());
        }
    }

}