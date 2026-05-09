/**
 * 
 *   use SeetAlert2 Library
 */

function showToast(msg, iconType = 'success') {
    const Toast = Swal.mixin({
        toast: true,
        position: 'bottom-start', // المكان: أعلى اليمين
        showConfirmButton: false,
        timer: 5000, // يختفي بعد 3 ثوانٍ
        timerProgressBar: true,
        background:'#333',
        color:'#fff',


        customClass: {
            title: 'my-toast-title' 
        }
    });

    Toast.fire({
        icon: iconType, // 'success', 'error', 'warning', 'info'
        title: msg
    });
}


//----------------------------------------------------





function apiGetCompanyByUserCompany(userCompany,onSuccess,onError){
 
      $.ajax({
            url:"/Companies/GetCompanyByUserCompany",
            method :"GET",
            data : {userCompany : userCompany},
            success:function(data)
            {
               onSuccess(data)    
            },
            error:function(xhr,status,error)
            {
                onError(error); 
            }
        }); 
}

function apiGetAllBranches(companyId,onSuccess,onError){
 
      $.ajax({
            url:"/Branches/GetAllBranches",
            method :"GET",
            data : {companyId : companyId},
            success:function(data)
            {
               onSuccess(data)    
            },
            error:function(xhr,status,error)
            {
                onError(error); 
            }
        }); 
}

function apiGetAllPosPoints(companyId,branchId,onSuccess,onError){
        
    console.log("Fetching POS points for branchId: " + branchId); // Debug log
    $.ajax({
            url:"/PosPoints/GetAllPosPoints",
            method :"GET",
            data : {companyId : companyId, branchId : branchId},

            success:function(data)
            {
               onSuccess(data)    
            },
            error:function(xhr,status,error)
            {
                onError(error); 
            }
        }); 
}

function apiGetAllUsers(companyId,branchId,onSuccess,onError){
 
    $.ajax({
            url:"/Users/GetAllUsers",
            method :"GET",
            data : {
                companyId : companyId,
                branchId : branchId
            },
            success:function(data)
            {
               onSuccess(data)    
            },
            error:function(xhr,status,error)
            {
                onError(error); 
            }
        });     
}

function apiLogin(userData,onSuccess,onError) {
 
    $.ajax({
        url:"/Home/Login",
        method :"Post",
        contentType : "application/json",
        data :JSON.stringify(userData),
        success:function(data)
        {
            onSuccess(data)    
        },
        error:function(xhr,status,error)
        {
            onError(error); 
        }
    });  

}

function apiSetAuditLogin(transLogin,onSuccess,onError){

    $.ajax({
       url:"/AttendanceLogs/SetAuditLogin",
       method:"POST",
       contentType:"Application/json",
       data:JSON.stringify(transLogin),
       success:function(data){onSuccess(data)},
       error:function(error){onError(error)}

    }); 
}

function apiGetUsersPrivileges(userId,onSuccess,onError){
  
    $.ajax({
        url:"/Users/GetUserPrivileges/" + userId,     
        method :"GET",
        data : {userId : userId},
        success:function(data)
        {    
            onSuccess(data)    
        },
        error:function(xhr,status,error)
        {            onError(error); 
        }       
     });
};

//---------------------------------------------------------------------

function apiGetIdAndNameCompanies(userCompany,onSuccess,onError){
  
    $.ajax({
        url:"/Companies/GetIdAndNameCompanies", 
        method :"GET",
        data : {userCompany : userCompany},
        success:function(data)
        {   onSuccess(data)     },          
        error:function(xhr,status,error)
        {            onError(error); 
        }       
     });
};

function apiGetIdAndNameBranches(companyId,onSuccess,onError){
 
      $.ajax({
            url:"/Branches/GetIdAndNameBranches",
            method :"GET",
            data : {companyId : companyId},
            success:function(data)
            {
               onSuccess(data)    
            },
            error:function(xhr,status,error)
            {
                onError(error); 
            }
        }); 
}          

function apiGetIdAndNamePosPoints(companyId,branchId,onSuccess,onError){
   
  
   
    $.ajax({
            url:"/PosPoints/GetIdAndNamePosPoints",
            method :"GET",
            data : {companyId : companyId, branchId : branchId},
            success:function(data)
            {
               onSuccess(data)    
            },
            error:function(xhr,status,error)
            {             onError(error);   

            }
        }); 
}   

function apiGetIdAndNameUsersByCompanyAndBranch(companyId,branchId,onSuccess,onError){
   

    $.ajax({
            url:"/Users/GetIdAndNameUsers",
            method :"GET",
            data : {companyId : companyId, branchId : branchId},
            success:function(data)
            {
               onSuccess(data)    
            },
            error:function(xhr,status,error)
            {             onError(error);   

            }
        }); 
}   

function apiGetIdAndNameUsersByCompanyAndBranchAndPos(companyId,branchId,posId,onSuccess,onError){
   
  
    $.ajax({
            url:"/Users/GetIdAndNameUsers",
            method :"GET",
            data : {companyId : companyId, branchId : branchId, posId : posId},
            success:function(data)
            {
               onSuccess(data)    
            },
            error:function(xhr,status,error)
            {             onError(error);   

            }
        }); 
}  

function apiGetIdAndNamePaymentMethods(onSuccess,onError){
      
    $.ajax({
            url:"/PaymentMethods/GetIdAndNamePaymentMethods",
            method :"GET",
            success:function(data)
            {
               onSuccess(data)    
            },
            error:function(xhr,status,error)
            {             onError(error);   

            }
        }); 
}

// ------------------------------------------------------------

function apiGetAllCustomers(onSuccess,onError){
    $.ajax({
        url:"/Customers/GetAllCustomers",
        method:"GET",
        success: function(data) {
            onSuccess(data); 
        },
        error:function(xhr,status,error) {
                onError(error);
        }
        
        }); 

} 

function apiGetAllCategories(onSuccess,onError){
    $.ajax({
    url:"/Categories/GetAllCategories",
    method:"GET",
    success: function(data) {
        onSuccess(data); 
    },
    error:function(xhr,status,error) {
            onError(error);
            
    }
    
    });
}

function apiGetAllProducts(onSuccess,onError) {
    $.ajax({
        url:"/Products/GetAllProducts",
        method:"GET",
        success: function(data) {
            onSuccess(data); 
        },
        error:function(xhr,status,error) {
                onError(error);
                
        }
        
        });    
}

function apiGetProductsForOperations(onSuccess,onError)
{
    $.ajax({
        url:"/Products/GetProductsForOperation",
        method:"GET",
        success: function(data) {
            onSuccess(data); 
        },
        error:function(xhr,status,error) {
                onError(error);
                
        }

    });    
}

function apiGetAllPendingInvoices(onSuccess, onError) {
    $.ajax({
        url:"/PendingInvoices/GetAllPendingInvoices",
        method:"GET",
        success: function(data) {
            onSuccess(data); 
        },
        error:function(xhr,status,error) {
                onError(error);
                
        }
        
        });
}

function apiGetAllSalesInvoices(onSuccess,onError) {
   
    $.ajax({
        url:"/SalesInvoices/GetAllSalesInvoices",
        method:"GET",
        success: function(data) {
            onSuccess(data); 
        },
        error:function(xhr,status,error) {
                onError(error);
                
        }
        
    });   
}

function apiGetAllPurchasesInvoices(onSuccess,onError) {
      
    $.ajax({
        url:"PruchasesInvoices/GetAllPruchasesInvoices",
        method:"GET",
        success: function(data) {
            onSuccess(data); 
        },
        error:function(xhr,status,error) {
                onError(error);
                
        }
        
    });   
}


function apiGetCustomer(id,onSuccess, onError) {
      
    $.ajax({
            url:"/Customers/GetCustomer/" + id ,
            method:"GET",
            success: function(data) {
                onSuccess(data); 
            },
            error:function(xhr,status,error) {
                 onError(error);
                 
            }
            
    });
}


function apiGetCategory(id,onSuccess, onError) {
      
    $.ajax({
        url:"/Categories/GetCategory" + id,
        method:"GET",
        success: function(data) {
            onSuccess(data); 
        },
        error:function(xhr,status,error) {
                onError(error);
                
        }
        
     });
}

function apiGetProduct(id,onSuccess, onError) {
      
    $.ajax({
        url:"/Prodcuts/GetProduct/" + id,
        method:"GET",
        success: function(data) {
            onSuccess(data); 
        },
        error:function(xhr,status,error) {
            onError(error);
            
        }
        
    });
}

function apiGetSaleInvoice(id, onSuccess, onError){
        $.ajax({
        url:"/SalesInvoices/GetSaleInvoice/" + id,
        method:"GET",
        success: function(data) {
            onSuccess(data); 
        },
        error:function(xhr,status,error) {
            onError(error);
            
        }
        
    });
}

function apiGetPendingInvoice(id, onSuccess, onError){
   
    $.ajax({
    url:"/PendingInvoices/GetPendingInvoice/"  + id ,
    method:"GET",
    success: function(data) {
        onSuccess(data); 
    },
    error:function(xhr,status,error) {
            onError(error);
            
    }
    
    });
}




// -----------------------------------------------------------


function apiAddCustomer(customerData,onSuccess,onError){
 
    $.ajax({
        url: "/Customers/AddCustomer", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(customerData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}


function apiAddSupplier(supplierData,onSuccess,onError){
 
    $.ajax({
        url: "/Suppliers/AddSupplier", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(supplierData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}


function apiAddPendingInvoice(pendingData,onSuccess,onError){
 
    $.ajax({
        url: "/PendingInvoices/AddPendingInvoice", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(pendingData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiAddSaleInvoice(SaleData,onSuccess,onError){
 
    $.ajax({
        url: "/SalesInvoices/Create", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(SaleData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiAddPurchaseInvoice(purchaseData,onSuccess,onError){
 
    $.ajax({  
        url:"/Purchases/AddInvoice",
        method:"POST",
        contentType:"Application/json",
        data:JSON.stringify(purchaseData),
        success:function(response){
            onSuccess(response);
        },
        error:function(xhr,status,error){
            onError(error); 
        }
      }); 
}

function apiAddExpense(ExpenseData,onSuccess,onError){
 
    $.ajax({    }); 
}   

function apiAddUser(UserData,onSuccess,onError){
 
    $.ajax({    }); 
}

function apiAddCompany(CompanyData,onSuccess,onError){
 
    $.ajax({    }); 
}  

function apiAddBranch(BranchData,onSuccess,onError){
 
    $.ajax({    }); 
}   

function apiAddPosPoint(PosPointData,onSuccess,onError){
 
    $.ajax({    }); 
}   

function apiAddUnit(unitData,onSuccess,onError){
 
    $.ajax({  

        url: "/Units/AddUnit",
        method: "POST",
        contentType: "application/json",    
        data: JSON.stringify(unitData),
        success: function(response) {
            onSuccess(response);    
        },
        error: function(xhr, status, error) {
            onError(error);
        }

      }); 
}
function apiAddCategory(categoryData,onSuccess,onError){
 
    $.ajax({
        url: "/Categories/AddCategory", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(categoryData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiAddProduct(productData,onSuccess,onError){
 
    $.ajax({
        url: "/Products/AddProduct", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(productData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

// ---------------------------------------------------

function apiUpdateCustomer(customerData,onSuccess,onError){
 
    $.ajax({
        url: "/Customers/Edit", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(invoiceData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}


function apiUpdateSupplier(supplierData,onSuccess,onError){
 
    $.ajax({
        url: "/Suppliers/Edit", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(invoiceData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiUpdateCategory(categoryData,onSuccess,onError){
 
    $.ajax({
        url: "/Categories/Edit", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(invoiceData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}


function apiUpdateProduct(productData,onSuccess,onError){
 
    $.ajax({
        url: "/Products/Update", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(invoiceData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiUpdatePendingInvoice(pendingData,onSuccess,onError){
 
    $.ajax({
        url: "/PendingInvoices/Edit", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(invoiceData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiUpdateSaleInvoice(){
 
    $.ajax({
        url: "/SalesInvoices/Edit", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(invoiceData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiUpdateSaleInvoice(){
 
    $.ajax({
        url: "/SalesInvoices/Edit", 
        method: "POST",
        contentType: "application/json", 
        data: JSON.stringify(invoiceData), 
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

// ---------------------------------------------

function apiDeleteCustomer(customerId,onSuccess,onError){
 
    $.ajax({
        url: "/Customers/Delete/" + customerId, 
        method: "POST",
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiDeleteSupplier(supplierId,onSuccess,onError){
 
    $.ajax({
        url: "/Suppliers/Delete/" + supplierId, 
        method: "POST",
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiDeleteCategory(categoryId,onSuccess,onError){
 
    $.ajax({
        url: "/Categories/Delete/" + categoryId, 
        method: "POST",
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}


function apiDeleteProduct(productId,onSuccess,onError){
 
    $.ajax({
        url: "/Products/Delete/" + productId, 
        method: "POST",
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiDeletePendingInvoice(pendingId,onSuccess,onError){
 
    $.ajax({
        url: "/PendingInvoices/Delete/" + pendingId, 
        method: "POST",
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiDeleteSaleInvoice(saleId,onSuccess,onError){
 
    $.ajax({
        url: "/SalesInvoices/Delete/" +saleId , 
        method: "POST",
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}


// -----------------------------------------------------------

function apiGetSumSales(onSuccess,onError)
{
         $.ajax({
        url: "/SalesInvoices/GetSumSales", 
        method: "GET",
        success: function(data) {
            onSuccess(data); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiGetSumPurchases(onSuccess,onError)
{
         $.ajax({
        url: "/SalesInvoices/GetSumSales", 
        method: "GET",
        success: function(data) {
            onSuccess(data); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiGetSumExpenses(onSuccess,onError)
{
         $.ajax({
        url: "/SalesInvoices/GetSumSales", 
        method: "GET",
        success: function(data) {
            onSuccess(data); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

function apiGetSumNetCash(onSuccess,onError)
{
         $.ajax({
        url: "/SalesInvoices/GetSumNetCash", 
        method: "GET",
        success: function(data) {
            onSuccess(data); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}

// -----------------------------------------------------------
  /**
   * 
   *  Services For Products
   */


  function apiGetIdAndNameCategories(onSuccess,onError){

     $.ajax({
        url: "/Categories/GetIdAndNameCategories", 
        method: "GET",
        success: function(response) {   onSuccess(response); },
        error: function(xhr, status, error) { onError(error); }
      }); 
  }

function apiGetIdAndNameProducts(onSuccess,onError){
        $.ajax({   
        url: "/Products/GetIdAndNameProducts", 
        method: "GET",
        success: function(response) {   onSuccess(response); },
        error: function(xhr, status, error) { onError(error); }
       });


        
}

 function apiGetIdAndNameTaxies(onSuccess,onError){

     $.ajax({
        url: "/Taxies/GetIdAndNameTaxies", 
        method: "GET",
        success: function(response) {   onSuccess(response); },
        error: function(xhr, status, error) { onError(error); }
     })
 }

  function apiGetIdAndNameCustomers(onSuccess,onError){

     $.ajax({
        url: "/Customers/GetIdAndNameCustomers", 
        method: "GET",
        success: function(response) {   onSuccess(response); },
        error: function(xhr, status, error) { onError(error); }
      }); 
  }

  function apiGetIdAndNameSuppliers(onSuccess,onError){

     $.ajax({ 
        url: "/Suppliers/GetIdAndNameSuppliers", 
        method: "GET",
        success: function(response) {   onSuccess(response); },
        error: function(xhr, status, error) { onError(error); }
       }); 
  }



  function apiGetIdAndNameUnits(onSuccess,onError){

     $.ajax({ 
        url: "/Units/GetIdAndNameUnits", 
        method: "GET",
        success: function(response) {   onSuccess(response); },
        error: function(xhr, status, error) { onError(error); }
       }); 
  }

  function apiGetTaxRateForProduct(productId,onSuccess,onError){

     $.ajax({
        url: "/Products//" + productId, 
        method: "POST",
        success: function(response) {
            onSuccess(response); 
        },
        error: function(xhr, status, error) {
            onError(error); 
        }
    });
}





