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

        $("#btnFetchData").on("click",function(e){
                PosSession.userCompany = $("#userCompany").val(); 
                GetInfoCompanyByUserCompany(PosSession.userCompany);
        });

        $("#BranchId").on("change",function(){
        
            PosSession.branchId = $(this).val();   
            GetAllPosPoints();
            GetAllUsers(); 
        }); 
   
        $("#PosId").on("change",function(){

            PosSession.posId = $(this).val(); 
        
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

          apiGetCompanyByUserCompany(
            userCompany,
            function(data)
            {
                PosSession.companyId = data.companyId; 
                GetInfoBranchesByCompanyId();  
            },
            function(error)
            {
                $("#MsgError").text("فشل في جلب بيانات شركتك من الخادم  " + error);
            }
          )
}

function GetInfoBranchesByCompanyId(){

        apiGetAllBranches(
            PosSession.companyId,
            function(data)
            {
                var branchSelect = $("#BranchId");
                branchSelect.empty(); 
                branchSelect.append('<option value="" disabled selected>اختر الفرع</option>'); // خيار افتراضي

                data.forEach(branch=> {
                        branchSelect.append(`<option value="${branch.branchId}"> ${branch.branchName}</option>`);
                });  
            },
            function(error)
            {
                $("#MsgError").text("حدث خطأ أثناء جلب الفروع . " + error);
            }
        )

}

function GetAllPosPoints(){
  
    apiGetAllPosPoints(
        PosSession.companyId,
        PosSession.branchId,
        function(data)
        {
            var PosPointsSelect = $("#PosId"); 
            PosPointsSelect.empty(); 
            PosPointsSelect.append('<option value disable select>اختر النقطة</option>')
            
            data.forEach(p=>{
                PosPointsSelect.append(`<option value="${p.posId}">${p.posName}</option>`); 
            }); 
        },
        function(error)
        {
            $("#MsgError").text("حدث خطأ أثناء جلب  نقاط البيع . " + error);
        }
  )
}

function GetAllUsers(){
    
      apiGetAllUsers(
           PosSession.companyId,
           PosSession.branchId,
           function(data)
           {
                var UsersSelect = $("#username");
                UsersSelect.empty(); 
                UsersSelect.append('<option value  disable select>اختر المستخدم</option>');

                data.forEach(user=>{
                    UsersSelect.append(`<option value="${user.userId}">${user.userName}</option>`);
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
        function(data)
        {
            if(data.roleId == 1)
            { window.location.href = "/Home/Dashboard";}
            else if(data.roleId == 2 || data.roleId == 3 )
            {window.location.href = "/Home/Cashier"}
        },
        function(error)
        {
            $("#MsgError").text("خطأ في عملية تسجيل الدخول  " + error)
        }
     )
}
  