$(document).ready(function () {
  
    $('.user-radio').change(function () {
        var userId = $(this).val();
       
        // إظهار رسالة تحميل
        $('#privilegesContainer').html('<div class="spinner-border text-primary"></div> جاري التحميل...');
         
        apiGetUsersPrivileges(
            userId,
            function (data) {   
                  
                var body = $("#privilegesTableBody");
                body.empty(); // مسح المحتوى السابق

                if (data.length === 0) {     
                    $('#privilegesContainer').html('<div class="alert alert-warning">لا توجد صلاحيات لهذا المستخدم</div>');
                    return;
                   }

                 data.privileges.forEach(p => {
                    var row =
                    `<tr> 
                         <td>  ${p.privilegeId} </td> 
                        <td>  ${p.privilegeName} </td>   
                        <td>  ${p.description} </td>   
                        <td> <input type="checkbox" value="${p.privilegeId}" id="select-privilege" checked/> </td>
                    </tr>`;
                   
                    body.append(row);
                });

                $('#privilegesContainer').html(''); // إزالة رسالة التحميل
            },
            function () {
                $('#privilegesContainer').html('<div class="alert alert-danger">خطأ في جلب البيانات</div>');
            }
        );
    });

});