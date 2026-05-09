 /** --------------------
  *    Page Login 
  *  --------------------
  */
 

 var PosSession = {
    userCompany : null,
    companyId : null,
    branchId : null, 
    posId : null,
    userId : null,
    userPassword :null
 };



 $(document).ready(function(){
   
        if($("#userCompany").length === 0) return; 
         


        console.log("login Page");
        console.log(localStorage.getItem("loginType")); 
        console.log(localStorage.getItem("loginMessage")); 
        console.log("Test " + Number(window.PosSession.loginTypeId)); 
        console.log(Number(window.PosSession.userId)); 
        console.log(window.PosSession.userName); 

        let msg = localStorage.getItem("loginMessage");
   
        if (msg) {
            showToast(msg); 
            localStorage.removeItem("loginMessage");
        }

        $("#btnFetchData").on("click",function(e){
                PosSession.userCompany = $("#userCompany").val(); 
                GetInfoCompanyByUserCompany(PosSession.userCompany);
        });

        $("#BranchId").on("change",function(){
            PosSession.branchId = $(this).val();   
            GetInfoPosPointsByBranchId();
            GetInfoUsersByCompanyIdAndBranchId(); 
        }); 
   
        $("#PosId-select").on("change",function(){
            PosSession.posId = $(this).val(); 
            GetInfoUsersByCompanyIdAndBranchIdAndPosId();
        }); 

        $("#username").on("change",function(){
            PosSession.userId = $(this).val(); 
        });

        $("#showPassword").change(function(){
            if($(this).is(":checked")){
                $("#password").attr("type","text");
            }else{
                $("#password").attr("type","password");
            } 
        });

        $("#btnLogin").click(function(){

            loginUser();  
        }); 

 }); 

function GetInfoCompanyByUserCompany(userCompany){  

          apiGetIdAndNameCompanies(
            userCompany,
            function(data)
            {
                PosSession.companyId = data.id; 
                GetInfoBranchesByCompanyId();  
            },
            function(error)
            {
                $("#MsgError").text("فشل في جلب بيانات شركتك من الخادم  " + error);
            }
          )
}

function GetInfoBranchesByCompanyId(){

        apiGetIdAndNameBranches(
            PosSession.companyId,
            function(data)
            {
                var branchSelect = $("#BranchId");
                branchSelect.empty(); 
                branchSelect.append('<option value="" disabled selected>اختر الفرع</option>'); // خيار افتراضي

                data.forEach(branch=> {
                        branchSelect.append(`<option value="${branch.id}"> ${branch.name}</option>`);
                });  
            },
            function(error)
            {
                $("#MsgError").text("حدث خطأ أثناء جلب الفروع . " + error);
            }
        )

}

function GetInfoPosPointsByBranchId(){
  
    apiGetIdAndNamePosPoints(
        PosSession.companyId,
        PosSession.branchId,
        function(data)
        {
            var PosPointsSelect = $("#PosId-select"); 
            PosPointsSelect.empty(); 
            PosPointsSelect.append('<option value disable select>اختر النقطة</option>')
            
            data.forEach(p=>{
                PosPointsSelect.append(`<option value="${p.id}">${p.name}</option>`); 
            }); 
        },
        function(error)
        {
            $("#MsgError").text("حدث خطأ أثناء جلب  نقاط البيع . " + error);
        }
  )
}

function GetInfoUsersByCompanyIdAndBranchId(){
    
      apiGetIdAndNameUsersByCompanyAndBranch(
           PosSession.companyId,
           PosSession.branchId,
           function(data)
           {
                var UsersSelect = $("#username");
                UsersSelect.empty(); 
                UsersSelect.append('<option value  disable select>اختر المستخدم</option>');

                data.forEach(user=>{
                    UsersSelect.append(`<option value="${user.id}">${user.name}</option>`);
                }); 
           },
           function(error)
           {
                $("#MsgError").text("حدث خطأ أثناء جلب  بيانات المستخدم . " + error);
           }
      ); 
    
}

function GetInfoUsersByCompanyIdAndBranchIdAndPosId(){
    
      apiGetIdAndNameUsersByCompanyAndBranchAndPos(
           PosSession.companyId,
           PosSession.branchId,
           PosSession.posId,
           function(data)
           {
                var UsersSelect = $("#username");
                UsersSelect.empty(); 
                UsersSelect.append('<option value  disable select>اختر المستخدم</option>');

                data.forEach(user=>{
                    UsersSelect.append(`<option value="${user.id}">${user.name}</option>`);
                }); 
           },
           function(error)
           {
                $("#MsgError").text("حدث خطأ أثناء جلب  بيانات المستخدم . " + error);
           }
      ); 
    
}


function loginUser(){
   
    var userdata = {
        companyId :parseInt(PosSession.companyId),
        branchId : parseInt(PosSession.branchId),
        posId : parseInt(PosSession.posId) || 0,
        userId :parseInt(PosSession.userId),
        userPassword : $("#password").val() ,
    }
            
    apiLogin(
        userdata, 
        function(response) { // response هو الكائن الكامل
           if (response.success) {
               let user = response.data; // هنا نصل لبيانات المستخدم

                if (user.roleId == 1) {
                    window.location.href = "/Home/Dashboard";
                } 
                else if (user.roleId == 2 || user.roleId == 3) {
                         RegisterLoginForUser(user.userId,user.userName); 
                }
          } else {
             // إظهار الرسالة القادمة من الخادم (سواء حساب مقفل أو بيانات خطأ)
             $("#MsgError").removeClass("d-none").text(response.message);
             $("#MsgError").text(response.message);
          }
       }, 
        function(error) {
             $("#MsgError").removeClass("d-none").text("حدث خطأ أثناء محاولة تسجيل الدخول. ");
        }
    );
}
  
function RegisterLoginForUser(userId,userName){
   

    var transLogin = {
        userId : userId,
        userName : userName,
        logTypeId : 0,
        notes: ""
    }; 


    console.log(transLogin); 

    apiSetAuditLogin(
        transLogin,
        function(response){
            if(response.success)
            {
               localStorage.setItem("loginType",response.loginType); 
               localStorage.setItem("loginMessage",response.message);
                 window.location.href = "/Home/Cashier";

            }
        },
        function(error){
            localStorage.setItem("loginType",response.loginType); 
            localStorage.setItem("loginMessage",response.message)
        }
    )



}