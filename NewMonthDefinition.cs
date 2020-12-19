  string FiscalYearInitialDate = string.Empty;
            int[] MonthOfFy = { 10, 11, 12, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            string[] MonthYear = { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" };
            int seq = 1;

            Sql("DELETE month");

            for (int y = 15; y <= 25; y++)
            {
                int FiscalMonthOrder = 1;


                foreach (int m in MonthOfFy)
                {

                    string startdate = string.Empty;
                    string endate = string.Empty;
                    string yearnumber = string.Empty;

                    if (m <= 9)
                    {
                        startdate = (2000 + (y + 1)) + "-" + m + "-21";
                        yearnumber = (2000 + (y + 1)).ToString();
                    }
                    else
                    {
                        startdate = (2000 + y) + "-" + m + "-21";
                        yearnumber = (2000 + y).ToString();
                    }

                    if (m == 12)
                    {
                        endate = (2000 + (y + 1)) + "-" + (1) + "-20";
                    }
                    else
                    {
                        endate = (2000 + y) + "-" + (m + 1) + "-20";

                        if (m <= 9)
                        {
                            endate = (2000 + (y + 1)) + "-" + (m + 1) + "-20";
                        }
                    }

                    string monthDescription = string.Format("{0} à {1}", startdate, endate);
                    int fiscalyear = (2000 + (y + 1));
                    int MonthNumber = m;
                    string DHIS2MonthFormat = string.Empty;

                    if (m > 9)
                    {
                        DHIS2MonthFormat = ((2000 - 1) + (y + 1)) + "" + m;
                    }
                    else
                    {
                        DHIS2MonthFormat = (2000 + (y + 1)) + "" + m;
                    }

                    if (FiscalMonthOrder == 1)
                    {
                        FiscalYearInitialDate = startdate;
                    }
                   

                    Sql($@"

                                        INSERT INTO [Month] (StartDate,EndDate,Seq,monthDescription,fiscalmonthorder,fiscalyear,MonthNumber,FiscalYearInitialDate,DHIS2MonthFormat,MonthYear,yearnumber,state,syncstate,CreatedDate)
                                        VALUES              ('{startdate}'  ,
                                                             '{endate}'  ,
                                                             {seq}  ,
                                                             '{monthDescription}'  ,
                                                             '{FiscalMonthOrder}'  ,
                                                             '{fiscalyear}'  ,
                                                             '{MonthNumber}'  ,
                                                             '{FiscalYearInitialDate}'  ,
                                                             {DHIS2MonthFormat}  ,
                                                             '{MonthYear[m - 1] + " - " + yearnumber}'  ,
                                                             {yearnumber}
                                                             ,0,-1,Getdate())
                                       ");

                    seq++;
                    FiscalMonthOrder++;

                }
            }