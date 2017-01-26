window.dao = {

    syncURL: "../api/custfeedback/PostFeedbackList",

    CheckConnection: function(callback)
    {
        var randomValue = Math.floor((1 + Math.random()) * 0x10000)
        $.ajax({
            type: "GET",
            url: "../api/custfeedback/GetConnection?rand=" + randomValue,
            error: function () { callback(false); },
            success: function () { callback(true); }
        });
    },
    initialize: function (callback) {
        var self = this;
        this.db = window.openDatabase("synccxpdb", "1.0", "Sync CXP DB", 200000);

        // Testing if the table exists is not needed and is here for logging purpose only. We can invoke createTable
        // no matter what. The 'IF NOT EXISTS' clause will make sure the CREATE statement is issued only if the table
        // does not already exist.
        this.db.transaction(
            function (tx) {
                tx.executeSql("SELECT name FROM sqlite_master WHERE type='table' AND name='feedback'", this.txErrorHandler,
                    function (tx, results) {
                        if (results.rows.length == 1) {
                            //log('Using existing Feedback table in local SQLite database');
                            //self.dropTable(callback);
                            return;
                        }
                        else {
                            //log('Feedback table does not exist in local SQLite database');
                            self.createTable(callback);
                        }
                    });
            }
        )

    },
    createTable: function (callback) {
        this.db.transaction(
            function (tx) {
                var sql =
                    "CREATE TABLE IF NOT EXISTS feedback ( " +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "SmileyActionID INTEGER, " +
                    "CategoryID INTEGER, " +
                    "ServiceID INTEGER, " +
                    "DeviceID VARCHAR(50), " +
                   
                    "DateAdded VARCHAR(50),"+
                    "CommentInclude VARCHAR(10), "+
                    "FullName VARCHAR(50), " +
                    "Email VARCHAR(50), " +
                    "PhoneNo VARCHAR(50), "+
                    "Comment VARCHAR(1000))";
                tx.executeSql(sql);
            },
            this.txErrorHandler,
            function () {
               // log('Table feedback successfully CREATED in local SQLite database');
                callback();
            }
        );
    },

    dropTable: function (callback) {
        this.db.transaction(
            function (tx) {
                tx.executeSql('DROP TABLE IF EXISTS feedback');
            },
            this.txErrorHandler,
            function () {
                //log('Table employee successfully DROPPED in local SQLite database');
                callback();
            }
        );
    },
    AddFeedback: function(feedback, callback) {
            this.db.transaction(
                function(tx) {
                    //var l = employees.length;
                    var sql =
                        "INSERT OR REPLACE INTO feedback (SmileyActionID, CategoryID, ServiceID, DeviceID, DateAdded, CommentInclude,FullName,Email,PhoneNo,Comment) " +
                        "VALUES (?, ?, ?, ?, ?, ?, ?,?,?,?)";
                    //log('Inserting or Updating in local database:');
                    var e= JSON.parse(feedback);
                   
                    var params = [e['SmileyActionID'], e['CategoryID'], e['ServiceID'], e['DeviceID'], e['DateAdded'], e['CommentInclude'], e['FullName'], e['Email'], e['PhoneNo'], e['Comment']];
                        tx.executeSql(sql, params);
                   
                },
                this.txErrorHandler,
                function(tx) {
                    callback();
                }
            );
        },
    GetAllFeedback: function (callback) {
        this.db.transaction(
            function (tx) {
                var sql = "SELECT * FROM Feedback";
               // log('Local SQLite database: "SELECT * FROM feedback"');
                tx.executeSql(sql, this.txErrorHandler,
                    function (tx, results) {
                        var len = results.rows.length,
                            feedbackJson = [];
                        for (var i = 0; i < results.rows.length; i++) {
                            ID=results.rows.item(i).ID;
                            SmileyActionID = results.rows.item(i).SmileyActionID;
                            CategoryID = results.rows.item(i).CategoryID;
                            ServiceID = results.rows.item(i).ServiceID;
                            DeviceID = results.rows.item(i).DeviceID;
                            DateAdded = results.rows.item(i).DateAdded;
                            
                            CommentInclude = results.rows.item(i).CommentInclude;
                            FullName = results.rows.item(i).FullName;
                            Email = results.rows.item(i).Email;
                            PhoneNo = results.rows.item(i).PhoneNo;
                            Comment = results.rows.item(i).Comment;
                            feedbackJson.push({ID:ID, SmileyActionID: SmileyActionID, CategoryID: CategoryID, ServiceID: ServiceID, DeviceID: DeviceID, DateAdded: DateAdded, CommentInclude: CommentInclude, FullName: FullName, Email: Email, PhoneNo: PhoneNo, Comment: Comment });
                          }
                        //log(len + ' rows found');
                        callback(feedbackJson);
                    }
                );
            }
        );
    },
    DeleteFeedback: function (callback) {
        this.db.transaction(
                function (tx) {
                    //var l = employees.length;
                    var sql ="DELETE FROM feedback";
                    tx.executeSql(sql);
                },
                this.txErrorHandler,
                function (tx) {
                    callback();
                }
            );
    },
    PostFeedback:function(feedback,callback)
    {
        var self = this;
        $.ajax({
            url: self.syncURL,
            type: "Post",
            data: JSON.stringify(feedback),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                //$("#mpeAlertPopup").fadeIn(1000).delay(2000).fadeOut("slow");
                callback();
            },
            error: function (xhr, status) {
                if (xhr.status == 417) {
                    alert('An error occurred while trying to synchronise data!!! Device has NOT been configured. Kindly contact vendor');
                }
                else if (status == 'timeout' || status == 'error' || status == 'notmodified' || status == 'parsererror') {
                    alert('Synchronization Failed:   Kindly check the data connection!');
                }

            }
        });
    },
    Sync: function()
    {
        var self = this;
        self.GetAllFeedback(function (feedbacks) {
            if (feedbacks.length > 0) {
                self.PostFeedback(feedbacks, function () {
                    self.DeleteFeedback(function () {
                        alert("Synchronisation Completedly successfully");
                    });
                });
            } else {
                return;
            }
        })
    },
    txErrorHandler: function (tx) {
        alert(tx.message);
    }
}