F2_jsonpCallback_savory_geoTrack({
    "scripts": [
		"Products/savory_geoTrack/appclass.js"
	],
    "styles": [
		"Products/savory_geoTrack/app.css"
	],
    "apps": [{
        "data": {},

        "html": ['<div class="col-md-12">         <div>             <table class="tblClass hdrBorder table table-condensed" id="savory_geoTrack">                 <thead>                 </thead>                 <tbody>                     <tr>                         <td>                             <div class="productNameHdr pull-left">                                 <strong>Geo Tracking</strong>                             </div>                         </td>                     </tr>                 </tbody>             </table>             <input id="savory_geoTrack_100" type="hidden" value="Info" />              <input id="savory_geoTrack_1" type="hidden" value="UserId" />              <input id="savory_geoTrack_108" type="hidden" />         </div>         <input id="start" type="button" value="start" class="btn btn-success" style="margin-bottom:5px;" /><input id="stop" type="button" value="stop" class="btn btn-danger" style="margin-left:5px;margin-bottom:5px;" /><div id="mapcontainer" style="width: 100%; height: 400px; border:2px solid"></div>     </div>'].join("")
    }]
})