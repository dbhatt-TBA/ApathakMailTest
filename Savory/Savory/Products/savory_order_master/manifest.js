F2_jsonpCallback_savory_order_master({
    "scripts": [
		"Products/savory_order_master/appclass.js"
	],
    "styles": [
		"Products/savory_order_master/app.css"
	],
    "apps": [{
        "data": {},

        "html": ['<div class="col-md-12">         <div>             <table class="tblClass hdrBorder table table-condensed" id="savory_order_master">                 <thead></thead>                 <tbody>                     <tr>                         <td style="width: 15%;">                             <div class="productNameHdr pull-left">                                 <strong>Order Details</strong>                             </div>                         </td>                         <td style="text-align: left; width: 10%;"><strong>Order Date : </strong></td>                         <td style="text-align: left;">                             <input style="text-align: right; cursor: pointer;" id="savory_order_master_100" type="text" title="datepick" /></td>                         <td>                             <input id="savory_order_master_101" type="hidden" /></td>                     </tr>                     <tr>                         <td colspan="4">                             <hr />                         </td>                     </tr>                 </tbody>             </table>         </div>     </div>'].join("")
    }]
})