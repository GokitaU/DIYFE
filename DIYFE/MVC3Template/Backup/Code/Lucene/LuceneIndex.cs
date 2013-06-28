using System.Collections.Generic;
using System.Linq;

//using Model.MDM;
using Lucene.Net;
using System;

namespace MVC3Template.SearchIndex
{
    public class LuceneIndex
    {
        public LuceneIndex()
        {
            this._indexFileLocation = AppStatic.LunceneIndexLocation;
            if (this._indexFileLocation == null)
            {
                throw(new Exception("No entry in the web.config for LuceneIndexLocaction - Errror in LuceneIndex.cs"));
            }
        }

        public LuceneIndex(string IndexFileLocation)
        {
            this._indexFileLocation = IndexFileLocation;
        }
        
        private string _indexFileLocation { get; set; }

        public bool GenerateIndex()
        {
            //string query = "SELECT A.MemberID, ISNULL(FirstName, '') AS FirstName, ISNULL(LastName,  '') AS LastName, ISNULL(CompanyName, '') AS CompanyName, ISNULL(Address, '') as Address, ISNULL(Email, '') as Email, ISNULL(PhonePersonal, '') as Phone, ISNULL(City, '') AS City, ISNULL(State, '') AS State,	ISNULL(PostalCode, '') AS PostalCode, ISNULL(SearchContent, '') as SearchContent "
            //    // "SELECT  A.MemberID, FirstName, LastName,  CompanyName, City, State,  PostalCode, SearchContent "
            //                + "FROM Member AS A CROSS APPLY "
            //    //+ "(SELECT cast(A.MemberId as varchar(255)) + ' ' + A.FirstName + ' ' + A.LastName + ' ' + A.CompanyName + ' ' + Product.ProductCode  + ' ' + Product.Description + ' ' "
            //                + "(SELECT Product.ProductCode  + ' ' + Product.Description + ' ' "
            //                + " FROM  Product INNER JOIN MemberProduct ON Product.ProductID = MemberProduct.ProductID "
            //                    + " WHERE MemberProduct.MemberID = A.MemberID FOR XML PATH('')) D(SearchContent)";

            //var LuceneDataList = new List<LuceneData>();

            //try
            //{

            //    using (var context = new MDMContext())
            //    {
            //        LuceneDataList = context.Database.SqlQuery<LuceneData>(query).ToList();
            //    };


            //    #region Lucene Index Data

            //    //state the file location of the index

            //    Lucene.Net.Store.Directory dir = Lucene.Net.Store.FSDirectory.GetDirectory(_indexFileLocation, true);

            //    //create an analyzer to process the text
            //    Lucene.Net.Analysis.Analyzer analyzer = new
            //    Lucene.Net.Analysis.Standard.StandardAnalyzer();

            //    //create the index writer with the directory and analyzer defined.
            //    Lucene.Net.Index.IndexWriter indexWriter = new
            //    Lucene.Net.Index.IndexWriter(dir, analyzer,
            //        /*true to create a new index*/ true);

            //    foreach (var dataObj in LuceneDataList)
            //    {

            //        dataObj.SearchContent += dataObj.FirstName + " " + dataObj.LastName + " " + dataObj.CompanyName + " " + dataObj.Address + " " + dataObj.Email + " " + dataObj.Phone + " " + dataObj.City + " " + dataObj.State + " " + dataObj.PostalCode;

            //        Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();

            //        Lucene.Net.Documents.Field fldContent =
            //          new Lucene.Net.Documents.Field("SearchContent",
            //                                         dataObj.SearchContent,
            //                                         Lucene.Net.Documents.Field.Store.YES,
            //                                         Lucene.Net.Documents.Field.Index.ANALYZED,
            //                                         Lucene.Net.Documents.Field.TermVector.YES);

            //        Lucene.Net.Documents.Field fldContent1 =
            //                  new Lucene.Net.Documents.Field("MemberID",
            //                                                dataObj.MemberID.ToString(),
            //                                                Lucene.Net.Documents.Field.Store.YES,
            //                                                Lucene.Net.Documents.Field.Index.NOT_ANALYZED,
            //                                                Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent2 =
            //              new Lucene.Net.Documents.Field("FirstName",
            //                                             dataObj.FirstName,
            //                                             Lucene.Net.Documents.Field.Store.YES,
            //                                             Lucene.Net.Documents.Field.Index.NO,
            //                                             Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent3 =
            //               new Lucene.Net.Documents.Field("LastName",
            //                                                 dataObj.LastName,
            //                                                 Lucene.Net.Documents.Field.Store.YES,
            //                                                 Lucene.Net.Documents.Field.Index.NO,
            //                                                 Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent4 =
            //               new Lucene.Net.Documents.Field("CompanyName",
            //                                                     dataObj.CompanyName,
            //                                                     Lucene.Net.Documents.Field.Store.YES,
            //                                                     Lucene.Net.Documents.Field.Index.NO,
            //                                                     Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent5 =
            //                   new Lucene.Net.Documents.Field("City",
            //                                                         dataObj.City,
            //                                                         Lucene.Net.Documents.Field.Store.YES,
            //                                                         Lucene.Net.Documents.Field.Index.NO,
            //                                                         Lucene.Net.Documents.Field.TermVector.NO);
            //        Lucene.Net.Documents.Field fldContent6 =
            //                   new Lucene.Net.Documents.Field("State",
            //                                                         dataObj.State,
            //                                                         Lucene.Net.Documents.Field.Store.YES,
            //                                                         Lucene.Net.Documents.Field.Index.NO,
            //                                                         Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent7 =
            //                   new Lucene.Net.Documents.Field("PostalCode",
            //                                                         dataObj.PostalCode,
            //                                                         Lucene.Net.Documents.Field.Store.YES,
            //                                                         Lucene.Net.Documents.Field.Index.NO,
            //                                                         Lucene.Net.Documents.Field.TermVector.NO);


            //        doc.Add(fldContent);
            //        doc.Add(fldContent1);
            //        doc.Add(fldContent2);
            //        doc.Add(fldContent3);
            //        doc.Add(fldContent4);
            //        doc.Add(fldContent5);
            //        doc.Add(fldContent6);
            //        doc.Add(fldContent7);
            //        //write the document to the index
            //        indexWriter.AddDocument(doc);
            //    }

            //    #endregion

            //    //optimize and close the writer
            //    indexWriter.Optimize();
            //    indexWriter.Commit();
            //    indexWriter.Close();
            //}
            //catch (Exception er)
            //{
            //    return false;
            //}


            return true;
        }

        public bool AppendShowGroup(int productSubGroupID, bool optimize)
        {
            return AppendShowGroup(0, productSubGroupID, optimize);
        }

        public bool AppendShow(int productID, bool optimize)
        {
            return AppendShowGroup(productID, 0, optimize);
        }

        private bool AppendShowGroup(int productID, int productSubGroupID, bool optimize)
        {
            //string query = "SELECT A.MemberID, ISNULL(FirstName, '') AS FirstName, ISNULL(LastName,  '') AS LastName, ISNULL(CompanyName, '') AS CompanyName, ISNULL(Address, '') as Address, ISNULL(Email, '') as Email, ISNULL(PhonePersonal, '') as Phone, ISNULL(City, '') AS City, ISNULL(State, '') AS State,	ISNULL(PostalCode, '') AS PostalCode, ISNULL(SearchContent, '') as SearchContent "
            //    // "SELECT  A.MemberID, FirstName, LastName,  CompanyName, City, State,  PostalCode, SearchContent "
            //    + "FROM Member AS A CROSS APPLY "
            //    //+ "(SELECT cast(A.MemberId as varchar(255)) + ' ' + A.FirstName + ' ' + A.LastName + ' ' + A.CompanyName + ' ' + Product.ProductCode  + ' ' + Product.Description + ' ' "
            //    + "(SELECT Product.ProductCode  + ' ' + Product.Description + ' ' "
            //    + " FROM  Product INNER JOIN MemberProduct ON Product.ProductID = MemberProduct.ProductID "
            //        + " WHERE MemberProduct.MemberID = A.MemberID FOR XML PATH('')) D(SearchContent) " +
            //    "WHERE ";

            //List<MemberProduct> members = new List<MemberProduct>();
            
            //if (productID > 0)
            //{

            //    using (var context = new MDMContext())
            //    {
            //       members = context.MemberProducts.Where(mp => mp.ProductID == productID).ToList();
            //    };
            //}
            //else if (productSubGroupID > 0)
            //{
            //    using (var context = new MDMContext())
            //    {
            //        members = context.MemberProducts.Where(mp => mp.Product.ProductGroupSubId == productSubGroupID).ToList();
            //    };
            //}
            //else
            //{
            //    return false;
            //}
            //if (members.Count > 0)
            //{
            //    foreach (MemberProduct mp in members)
            //    {
            //        query += "A.MemberID = " + mp.MemberID + " OR ";
            //    }

            //    query = query.Substring(0, (query.Length - 3));

            //}

            //var LuceneDataList = new List<LuceneData>();

            //try
            //{

            //    using (var context = new MDMContext())
            //    {
            //        LuceneDataList = context.Database.SqlQuery<LuceneData>(query).ToList();
            //    };


            //    #region Lucene Index Data

            //    //state the file location of the index

            //    Lucene.Net.Store.Directory dir = Lucene.Net.Store.FSDirectory.GetDirectory(_indexFileLocation, false);

            //    Lucene.Net.Index.IndexWriter indexWriter = new Lucene.Net.Index.IndexWriter(dir, new Lucene.Net.Analysis.Standard.StandardAnalyzer(), false);
            //    //create an analyzer to process the text
            //   // Lucene.Net.Analysis.Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer();

            //    //create the index writer with the directory and analyzer defined.
            //   // Lucene.Net.Index.IndexWriter indexWriter = new Lucene.Net.Index.IndexWriter(dir, analyzer, true);

            //    foreach (var dataObj in LuceneDataList)
            //    {

            //        Lucene.Net.Index.Term idTerm = new Lucene.Net.Index.Term("MemberID", dataObj.MemberID.ToString());
            //        indexWriter.DeleteDocuments(idTerm);
            //        //indexWriter.Commit();

            //        dataObj.SearchContent += dataObj.FirstName + " " + dataObj.LastName + " " + dataObj.CompanyName + " " + dataObj.Address + " " + dataObj.Email + " " + dataObj.Phone + " " + dataObj.City + " " + dataObj.State + " " + dataObj.PostalCode;

            //        Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();

            //        Lucene.Net.Documents.Field fldContent =
            //          new Lucene.Net.Documents.Field("SearchContent",
            //                                         dataObj.SearchContent,
            //                                         Lucene.Net.Documents.Field.Store.YES,
            //                                         Lucene.Net.Documents.Field.Index.ANALYZED,
            //                                         Lucene.Net.Documents.Field.TermVector.YES);

            //        Lucene.Net.Documents.Field fldContent1 =
            //                  new Lucene.Net.Documents.Field("MemberID",
            //                                                         dataObj.MemberID.ToString(),
            //                                                         Lucene.Net.Documents.Field.Store.YES,
            //                                                         Lucene.Net.Documents.Field.Index.NOT_ANALYZED,
            //                                                         Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent2 =
            //              new Lucene.Net.Documents.Field("FirstName",
            //                                             dataObj.FirstName,
            //                                             Lucene.Net.Documents.Field.Store.YES,
            //                                             Lucene.Net.Documents.Field.Index.NO,
            //                                             Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent3 =
            //               new Lucene.Net.Documents.Field("LastName",
            //                                                 dataObj.LastName,
            //                                                 Lucene.Net.Documents.Field.Store.YES,
            //                                                 Lucene.Net.Documents.Field.Index.NO,
            //                                                 Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent4 =
            //               new Lucene.Net.Documents.Field("CompanyName",
            //                                                     dataObj.CompanyName,
            //                                                     Lucene.Net.Documents.Field.Store.YES,
            //                                                     Lucene.Net.Documents.Field.Index.NO,
            //                                                     Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent5 =
            //                   new Lucene.Net.Documents.Field("City",
            //                                                         dataObj.City,
            //                                                         Lucene.Net.Documents.Field.Store.YES,
            //                                                         Lucene.Net.Documents.Field.Index.NO,
            //                                                         Lucene.Net.Documents.Field.TermVector.NO);
            //        Lucene.Net.Documents.Field fldContent6 =
            //                   new Lucene.Net.Documents.Field("State",
            //                                                         dataObj.State,
            //                                                         Lucene.Net.Documents.Field.Store.YES,
            //                                                         Lucene.Net.Documents.Field.Index.NO,
            //                                                         Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent7 =
            //                   new Lucene.Net.Documents.Field("PostalCode",
            //                                                         dataObj.PostalCode,
            //                                                         Lucene.Net.Documents.Field.Store.YES,
            //                                                         Lucene.Net.Documents.Field.Index.NO,
            //                                                         Lucene.Net.Documents.Field.TermVector.NO);


            //        doc.Add(fldContent);
            //        doc.Add(fldContent1);
            //        doc.Add(fldContent2);
            //        doc.Add(fldContent3);
            //        doc.Add(fldContent4);
            //        doc.Add(fldContent5);
            //        doc.Add(fldContent6);
            //        doc.Add(fldContent7);
            //        //write the document to the index
            //        indexWriter.AddDocument(doc);
            //    }

            //    #endregion

            //    //optimize and close the writer
            //    //Optimize take a while, it can be ran whenever but going to 
            //    if (optimize)
            //    {
            //        indexWriter.Optimize();
            //    }
            //    indexWriter.Commit();
            //    indexWriter.Close();
            //}
            //catch (Exception er)
            //{
            //    return false;
            //}


            return true;

        }

        public bool UpdateMember(int memberId)
        {
            //string query = "SELECT A.MemberID, ISNULL(FirstName, '') AS FirstName, ISNULL(LastName,  '') AS LastName, ISNULL(CompanyName, '') AS CompanyName, ISNULL(Address, '') as Address, ISNULL(Email, '') as Email, ISNULL(PhonePersonal, '') as Phone, ISNULL(City, '') AS City, ISNULL(State, '') AS State,	ISNULL(PostalCode, '') AS PostalCode, ISNULL(SearchContent, '') as SearchContent "
            //    // "SELECT  A.MemberID, FirstName, LastName,  CompanyName, City, State,  PostalCode, SearchContent "
            //   + "FROM Member AS A CROSS APPLY "
            //    //+ "(SELECT cast(A.MemberId as varchar(255)) + ' ' + A.FirstName + ' ' + A.LastName + ' ' + A.CompanyName + ' ' + Product.ProductCode  + ' ' + Product.Description + ' ' "
            //   + "(SELECT Product.ProductCode  + ' ' + Product.Description + ' ' "
            //   + " FROM  Product INNER JOIN MemberProduct ON Product.ProductID = MemberProduct.ProductID "
            //       + " WHERE MemberProduct.MemberID = A.MemberID FOR XML PATH('')) D(SearchContent) " +
            //   "WHERE A.MemberID = " + memberId;

            //var dataObj = new LuceneData();

            //try
            //{

            //    using (var context = new MDMContext())
            //    {
            //        dataObj = context.Database.SqlQuery<LuceneData>(query).FirstOrDefault();
            //    };


            //    #region Lucene Index Data

            //    //state the file location of the index

            //    Lucene.Net.Store.Directory dir = Lucene.Net.Store.FSDirectory.GetDirectory(_indexFileLocation, false);

            //    Lucene.Net.Index.IndexWriter indexWriter = new Lucene.Net.Index.IndexWriter(dir, new Lucene.Net.Analysis.Standard.StandardAnalyzer(), false);
            //    //create an analyzer to process the text
            //    // Lucene.Net.Analysis.Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer();

            //    //create the index writer with the directory and analyzer defined.
            //    // Lucene.Net.Index.IndexWriter indexWriter = new Lucene.Net.Index.IndexWriter(dir, analyzer, true);

            //    //foreach (var dataObj in LuceneDataList)
            //    //{

            //        Lucene.Net.Index.Term idTerm = new Lucene.Net.Index.Term("MemberID", dataObj.MemberID.ToString());
            //        indexWriter.DeleteDocuments(idTerm);
            //        //indexWriter.Commit();

            //        dataObj.SearchContent += dataObj.FirstName + " " + dataObj.LastName + " " + dataObj.CompanyName + " " + dataObj.Address + " " + dataObj.Email + " " + dataObj.Phone + " " + dataObj.City + " " + dataObj.State + " " + dataObj.PostalCode;

            //        Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();

            //        Lucene.Net.Documents.Field fldContent =
            //          new Lucene.Net.Documents.Field("SearchContent",
            //                                         dataObj.SearchContent,
            //                                         Lucene.Net.Documents.Field.Store.YES,
            //                                         Lucene.Net.Documents.Field.Index.ANALYZED,
            //                                         Lucene.Net.Documents.Field.TermVector.YES);

            //        Lucene.Net.Documents.Field fldContent1 =
            //                  new Lucene.Net.Documents.Field("MemberID",
            //                                                         dataObj.MemberID.ToString(),
            //                                                         Lucene.Net.Documents.Field.Store.YES,
            //                                                         Lucene.Net.Documents.Field.Index.NOT_ANALYZED,
            //                                                         Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent2 =
            //              new Lucene.Net.Documents.Field("FirstName",
            //                                             dataObj.FirstName,
            //                                             Lucene.Net.Documents.Field.Store.YES,
            //                                             Lucene.Net.Documents.Field.Index.NO,
            //                                             Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent3 =
            //               new Lucene.Net.Documents.Field("LastName",
            //                                                 dataObj.LastName,
            //                                                 Lucene.Net.Documents.Field.Store.YES,
            //                                                 Lucene.Net.Documents.Field.Index.NO,
            //                                                 Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent4 =
            //               new Lucene.Net.Documents.Field("CompanyName",
            //                                                     dataObj.CompanyName,
            //                                                     Lucene.Net.Documents.Field.Store.YES,
            //                                                     Lucene.Net.Documents.Field.Index.NO,
            //                                                     Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent5 =
            //                   new Lucene.Net.Documents.Field("City",
            //                                                         dataObj.City,
            //                                                         Lucene.Net.Documents.Field.Store.YES,
            //                                                         Lucene.Net.Documents.Field.Index.NO,
            //                                                         Lucene.Net.Documents.Field.TermVector.NO);
            //        Lucene.Net.Documents.Field fldContent6 =
            //                   new Lucene.Net.Documents.Field("State",
            //                                                         dataObj.State,
            //                                                         Lucene.Net.Documents.Field.Store.YES,
            //                                                         Lucene.Net.Documents.Field.Index.NO,
            //                                                         Lucene.Net.Documents.Field.TermVector.NO);

            //        Lucene.Net.Documents.Field fldContent7 =
            //                   new Lucene.Net.Documents.Field("PostalCode",
            //                                                         dataObj.PostalCode,
            //                                                         Lucene.Net.Documents.Field.Store.YES,
            //                                                         Lucene.Net.Documents.Field.Index.NO,
            //                                                         Lucene.Net.Documents.Field.TermVector.NO);


            //        doc.Add(fldContent);
            //        doc.Add(fldContent1);
            //        doc.Add(fldContent2);
            //        doc.Add(fldContent3);
            //        doc.Add(fldContent4);
            //        doc.Add(fldContent5);
            //        doc.Add(fldContent6);
            //        doc.Add(fldContent7);
            //        //write the document to the index
            //        indexWriter.AddDocument(doc);
            //   // }

            //    #endregion

            //    indexWriter.Commit();
            //    indexWriter.Close();
            //}
            //catch (Exception er)
            //{
            //    return false;
            //}


            return true;
            
        }

        public bool RemoveMember(int memberId)
        {

            try
            {

                Lucene.Net.Store.Directory dir = Lucene.Net.Store.FSDirectory.GetDirectory(_indexFileLocation, false);

                Lucene.Net.Index.IndexWriter indexWriter = new Lucene.Net.Index.IndexWriter(dir, new Lucene.Net.Analysis.Standard.StandardAnalyzer(), false);

                Lucene.Net.Index.Term idTerm = new Lucene.Net.Index.Term("MemberID", memberId.ToString());
                indexWriter.DeleteDocuments(idTerm);

                indexWriter.Commit();
                indexWriter.Close();
            }
            catch (Exception er)
            {
                return false;
            }


            return true;
            
        }

        public List<LuceneData> MemberSearch(string searchTerm)
        {
            var searchData = new List<LuceneData>();
            try
            {
                Lucene.Net.Store.Directory dir = Lucene.Net.Store.FSDirectory.GetDirectory(_indexFileLocation);


                //create an analyzer to process the text
                Lucene.Net.Analysis.Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer();

                //create the query parser, with the default search feild set to "content"
                Lucene.Net.QueryParsers.QueryParser queryParser = new Lucene.Net.QueryParsers.QueryParser("SearchContent", analyzer);

                //parse the query string into a Query object
                Lucene.Net.Search.Query query = queryParser.Parse(searchTerm);


                //create an index searcher that will perform the search
                //Lucene.Net.Index.IndexReader indexReader = Lucene.Net.Index.IndexReader.Open(dir, true);
                Lucene.Net.Search.IndexSearcher searcher = new Lucene.Net.Search.IndexSearcher(dir, true);

                ////build a query object
                //Lucene.Net.Index.Term luceneSearchTerm = new Lucene.Net.Index.Term("searchContent", searchTerm);
                //Lucene.Net.Search.Query query = new Lucene.Net.Search.TermQuery(luceneSearchTerm);

                //execute the query
                Lucene.Net.Search.Hits hits = searcher.Search(query);

                //int resultCount = hits.Length();
                //if (resultCount > 1000){
                //    resultCount = 1000;
                //}

                //iterate over the results.
                for (int i = 0; i < hits.Length(); i++)
                {
                    Lucene.Net.Documents.Document doc = hits.Doc(i);
                    searchData.Add(new LuceneData
                    {
                        MemberID = Convert.ToInt32(doc.Get("MemberID")),
                        FirstName = doc.Get("FirstName"),
                        LastName = doc.Get("LastName"),
                        CompanyName = doc.Get("CompanyName"),
                        City = doc.Get("City"),
                        State = doc.Get("State"),
                        PostalCode = doc.Get("PostalCode")
                    });
                }
            }
            catch (Exception ex)
            {
                
            }

            return searchData;
        }

    }
}