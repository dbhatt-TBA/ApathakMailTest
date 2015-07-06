 function ValidationReqFields(txt, id, type, msgid) {

            if (type == 'Email') {

                if (txt.value == '') {
                    document.getElementById(id).style.border = "1px solid #ff0000";
                    document.getElementById(msgid).style.display = '';
                    id.focus();
                    return false;
                }
                else {
                    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

                    if (!filter.test(txt.value)) {
                        document.getElementById(id).style.border = "1px solid #ff0000";
                        document.getElementById(msgid).style.display = '';
                        id.focus();
                        return false;
                    }
                    else {
                        document.getElementById(id).style.borderColor = "";
                        document.getElementById(msgid).style.display = 'none';
                        return false;
                    }
                }
            }
            else if (type == 'Blank') {

                if (txt.value == '') {
                    document.getElementById(id).style.border = "1px solid #ff0000";
                    document.getElementById(msgid).style.display = '';
                    id.focus();
                    return false;
                }
                else {
                    document.getElementById(id).style.borderColor = "";
                    document.getElementById(msgid).style.display = 'none';
                    return false;
                }

            }

            return false;

        }

        function ValidatePassword(txt, id, validateto, msgid, passwordmatch) {


            if (txt.value == '') {

                document.getElementById(id).style.border = "1px solid #ff0000";
                document.getElementById(msgid).style.display = '';
                id.focus();
                return false;
            }
            else if (txt.value != '' && document.getElementById(validateto).value == '') {

                document.getElementById(id).style.border = "";
                document.getElementById(msgid).style.display = 'none';
                id.focus();
                return false;
            }
            else if (txt.value != '' && document.getElementById(validateto).value != '') {

                document.getElementById(msgid).style.display = 'none';
                document.getElementById(id).style.border = "";
                document.getElementById(validateto).style.border = "";

                if (txt.value != document.getElementById(validateto).value) {
                    document.getElementById(id).style.border = "1px solid #ff0000";
                    document.getElementById(passwordmatch).style.display = '';
                    id.focus();
                    return false;
                }
                else {
                    document.getElementById(id).style.border = "";
                    document.getElementById(passwordmatch).style.display = 'none';
                    return false;
                }

                return false;
            }

        }
