import os

# Define the classes for which to create edit pages
classes = [
    'ToiletType',
    'ElevatorType',
    'ParkingType',
    'ParkingVisitor'
]

# Define the directory to store the .cshtml files
output_dir = ''

def ForCreate():
    for class_name in classes:
        file_name = f'{class_name}Create.cshtml'
        file_path = os.path.join(output_dir, file_name)
        
        with open(file_path, 'w') as file:
            ctcreate = f'''
            @model {class_name}
            @{{
                ViewBag.Title = "{class_name} Create";
                ViewBag.PageTitle = "{class_name} Create";
                Layout = "~/Views/Shared/_AdminLayout.cshtml";
            }}

            <div class="row">
                <div class="col-6">
                    <div class="card">
                        <div class="card-body">
                            <form action="/ProMaster/{class_name}Create" method="post" class="row g-0">

                                <div class="col-12">
                                    <div class="mb-3">
                                        <label asp-for="Name" class="from-label mb-2">Name</label>
                                        <input type="text" class="form-control" asp-for="Name" />
                                        <span asp-validation-for="Name" class="text-danger"></span>
                                    </div>
                                    <div class="mb-3">
                                        <label for="Status" class="from-label mb-2">Select Status</label>
                                        <select asp-for="Status" class="form-select">
                                            <option value="Active">Active</option>
                                            <option value="InActive">InActive</option>
                                        </select>
                                        <span asp-validation-for="Status" class="text-danger"></span>
                                    </div>
                                </div>
                                <div class="text-end">
                                    <button type="submit" class="btn btn-outline-primary">Submit</button>
                                    <button type="reset" class="btn btn-danger ms-3">Reset</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>

            @section Scripts {{
                <script>    
                </script>
            }}
        '''
            file.write(ctcreate)


def ForEdit():
    for class_name in classes:
        file_name = f'{class_name}Edit.cshtml'
        file_path = os.path.join(output_dir, file_name)
        
        with open(file_path, 'w') as file:
            ctedit = f'''
            @model {class_name}
            @{{
                ViewBag.Title = "{class_name} Edit";
                ViewBag.PageTitle = "{class_name} Edit";
                Layout = "~/Views/Shared/_AdminLayout.cshtml";
            }}

            <div class="row">
                <div class="col-6">
                    <div class="card">
                        <div class="card-body">
                            <form action="/ProMaster/{class_name}Edit" method="post" class="row g-0">
                                <input type="hidden" asp-for="Id" />
                                <div class="col-12">
                                    <div class="mb-3">
                                        <label asp-for="Name" class="from-label mb-2">Name</label>
                                        <input type="text" class="form-control" asp-for="Name" />
                                        <span asp-validation-for="Name" class="text-danger"></span>
                                    </div>
                                    <div class="mb-3">
                                        <label for="Status" class="from-label mb-2">Select Status</label>
                                        <select asp-for="Status" class="form-select">
                                            <option value="Active">Active</option>
                                            <option value="InActive">InActive</option>
                                        </select>
                                        <span asp-validation-for="Status" class="text-danger"></span>
                                    </div>
                                </div>
                                <div class="text-end">
                                    <button type="submit" class="btn btn-outline-primary">Submit</button>
                                    <button type="reset" class="btn btn-danger ms-3">Reset</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>

            @section Scripts {{
                <script>    
                </script>
            }}
        '''
            file.write(ctedit)



def ForList():
    for class_name in classes:
        file_name = f'{class_name}List.cshtml'
        file_path = os.path.join(output_dir, file_name)
        
        with open(file_path, 'w') as file:
            ctlist = f'''
            @model IEnumerable<{class_name}>
            @{{
                ViewBag.Title = "{class_name}";
                ViewBag.PageTitle = "{class_name}";
                Layout = "~/Views/Shared/_AdminLayout.cshtml";
                int i = 1;
            }}
            <div class="card">
                <div class="card-header">
                    <a href="/ProMaster/{class_name}Create" class="btn btn-success"><i class="bi bi-plus"></i> Add</a>
                </div>
                <div class="card-body table-responsive">
                    <table id="{class_name.lower()}" class="table table-striped table-bordered" style="width:100%">
                        <thead>
                            <tr class="text-nowrap">
                                <th>S. No</th>
                                <th>{class_name} Name</th>
                                <th>Status</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody class="text-nowrap">
                            @foreach (var item in Model)
                            {{
                                <tr>
                                    <td>@(i++)</td>
                                    <td>@item.Name</td>
                                    <td>@item.Status</td>
                                    <td>
                                        <a href="/ProMaster/{class_name}Edit/@item.Id" class="text-purple me-2"><i class="bi bi-pencil"></i></a>
                                        <a href="/ProMaster/{class_name}Delete/@item.Id" onclick="return confirm('Are you sure want to delete?')" class="text-danger"><i class="bi bi-trash"></i></a>
                                    </td>
                                </tr>
                            }}
                        </tbody>
                    </table>
                </div>
            </div>

            @section Scripts {{
                <script>
                    $('#{class_name.lower()}').DataTable({{
                        "scrollX": true
                    }});
                </script>
            }}
        '''
            file.write(ctlist)




ForEdit()
print(".cshtml files have been created.")
