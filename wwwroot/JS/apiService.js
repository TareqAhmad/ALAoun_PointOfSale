
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

function apiGetAllPruchasesInvoices(onSuccess,onError) {
      
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
        url: "/Customers/Create", 
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
        url: "/Suppliers/Create", 
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

function apiAddCategory(categoryData,onSuccess,onError){
 
    $.ajax({
        url: "/Categories/Create", 
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
        url: "/Products/Create", 
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

function apiGetSumnetCash(onSuccess,onError)
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





