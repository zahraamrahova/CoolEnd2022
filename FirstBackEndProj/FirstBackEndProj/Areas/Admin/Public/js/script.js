
$(document).ready(function () {

    $('#group-form').submit(function (ev) {

        ev.preventDefault();
        var form = $(this);

        var group = {

            Name: form.find("input[name=Name]").val(),
            AuthPermissions: []

        };

        var items = form.find(".action-permission");

        for (var i = 0; i < items.length; i++) {
            var perm = $(items[i]);
            permission = {
                AuthActionId: perm.data("action"),
                CanCreate: perm.find("input[name='CanCreate']").is(':checked'),
                CanView: perm.find("input[name='CanView']").is(':checked'),
                CanDelete: perm.find("input[name='CanDelete']").is(':checked'),
                CanEdit: perm.find("input[name='CanEdit']").is(':checked'),
                OnlyOwner: perm.find("input[name='OnlyOwner']").is(':checked')
            };


            for (var item in permission) {
                if (permission[item] === true) {
                    group.AuthPermissions.push(permission);
                    break;
                }

            }

        }
        console.log(group);

        $.ajax({
            url: form.attr("action"),
            method: form.attr("method"),
            data: {
                group: group
            },
            success: function (resp) {
                console.log(resp);
                if (resp.Success === true) {
                    toastr.success("Group created!");
                    $('#group-form')[0].reset();
                    
                }
                else {
                    toastr.warning(resp.Message);
                }

            },
            error: function () {

            }
        });

    });


    $('#edit-group-form').submit(function (ev) {

        ev.preventDefault();
        var form = $(this);

        var group = {

            Name: form.find("input[name=Name]").val(),
            Id: form.find("input[name=Id]").val()
         

        };
        var permissions= [];
        var items = form.find(".action-permission");

        for (var i = 0; i < items.length; i++) {
            var perm = $(items[i]);
            permission = {
                AuthGroupId: group.Id,
                AuthActionId: perm.data("action"),
                CanCreate: perm.find("input[name='CanCreate']").is(':checked'),
                CanView: perm.find("input[name='CanView']").is(':checked'),
                CanDelete: perm.find("input[name='CanDelete']").is(':checked'),
                CanEdit: perm.find("input[name='CanEdit']").is(':checked'),
                OnlyOwner: perm.find("input[name='OnlyOwner']").is(':checked')
            };


            for (var item in permission) {
                if (permission[item] === true) {
                    permissions.push(permission);
                    break;
                }

            }

        }
        console.log(group);

        $.ajax({
            url: form.attr("action"),
            method: form.attr("method"),
            data: {
                group: group,
                permissions: permissions
            },
            success: function (resp) {
                console.log(resp);
                if (resp.Success === true) {
                    toastr.success("Group Updated!");           
                  
                }
                else {
                    toastr.warning(resp.Message);
                }

            },
            error: function () {

            }
        });

    });

    $('#edit-user-form input[name=Status]').click(function() {
       
        if ($(this).is(":checked")) {
            $(this).val("true");
        }
        else {
            $(this).val("false");
        }

    });
   
    });

