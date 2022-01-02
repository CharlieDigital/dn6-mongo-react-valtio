import "./App.css";
import { Box, Button, Card, CardContent, Collapse, Grid, IconButton, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material";
import Add from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import { useSnapshot } from "valtio";
import React, { useEffect, useState } from "react";
import { appState } from "./store/AppState";
import { Company } from "./services";
import { CompanyVM } from "./viewModels/CompanyVM";

// Add a company to the global state.
async function addCompany()
{
    await appState.addRandomCompany();
}

// Represents an expandable row.
function Row(props: { company: Company })
{
    const { company } = props;
    const [open, setOpen] = useState(false);
    const [loaded, setLoaded] = useState(false);

    // Trigger on the initial load.
    useEffect(() =>
    {
        if (open && !loaded)
        {
            console.log(`Loading employees for company ${company.id}...`);

            (async () => await appState.loadEmployeesFor(company))()
                .catch(console.log);

            setLoaded(true);
        }
    });

    return (
        <React.Fragment>
            <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
                <TableCell>
                    <IconButton onClick={ () => setOpen(!open) } >
                        { open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon /> }
                    </IconButton>
                </TableCell>
                <TableCell><strong>{ company.label }</strong> ${ (new CompanyVM(company)).totalCompensation }</TableCell>
                <TableCell>{ company.id }</TableCell>
                <TableCell>
                    <IconButton onClick={ async() => await appState.deleteCompany(company) }>
                        <DeleteIcon />
                    </IconButton>
                </TableCell>
            </TableRow>
            <TableRow>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={ 4 }>
                    <Collapse in={ open } timeout="auto" unmountOnExit>
                        <Box sx={{ margin: 1 }}>
                            <Typography variant="h6" gutterBottom component="div">
                                Employees ({ company.employees?.length })
                                <Button
                                    sx={{ ml: 4 }}
                                    variant="outlined"
                                    size="small"
                                    startIcon={ <Add/> }
                                    onClick={ async() => await appState.addEmployeeTo(company) }
                                    disabled={ company.employees ? company.employees.length >= 10 : false }>
                                    Add Employee
                                </Button>
                            </Typography>
                            <Table size="small">
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Title</TableCell>
                                        <TableCell>First Name</TableCell>
                                        <TableCell>Last Name</TableCell>
                                        <TableCell>Compensation Name</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {
                                        company.employees?.map((employee) =>
                                        (
                                            <TableRow key={ employee.id }>
                                                <TableCell>{ employee.label }</TableCell>
                                                <TableCell>{ employee.firstName }</TableCell>
                                                <TableCell>{ employee.lastName }</TableCell>
                                                <TableCell>${ employee.salary }</TableCell>
                                            </TableRow>
                                        ))
                                    }
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </React.Fragment>
    );
}

// The main app UI.
function App()
{
    const { companies } = useSnapshot(appState);

    // Trigger on the initial load.
    useEffect(() =>
    {
        (async () => await appState.loadCompanies())()
            .catch(console.log);
    }, [""]);

    return (
        <Grid container spacing={ 3 }>
            <Grid item sm={ 0 } md={ 1 } lg={ 2 }>

            </Grid>
            <Grid item sm={ 12 } md={ 10 } lg={ 8 }>
                <Card
                    sx={{ mx: 1, my: 4 }}
                    variant="outlined">
                    <CardContent>
                        This app resets every 2 hours.  See the OpenAPI spec <a href={ `${import.meta.env.VITE_API_ENDPOINT}/swagger/index.html` }>here</a>.
                    </CardContent>
                    <CardContent>
                        Click the button to add a randomly generated Company.
                        <Button
                            sx={{ ml: 4 }}
                            variant="outlined"
                            size="small"
                            startIcon={ <Add/> }
                            onClick={ async () => await addCompany() }
                            disabled={ companies ? companies.length >= 10 : false }>
                            Add Company
                        </Button>
                    </CardContent>
                    <CardContent>
                        Showing { companies.length } companies (max 10; delete or reset to restart).  Total comp: <strong>${ appState.totalCompensation }</strong>

                        <Table size="small">
                            <TableHead>
                                <TableRow>
                                    <TableCell>+</TableCell>
                                    <TableCell>Company</TableCell>
                                    <TableCell>ID</TableCell>
                                    <TableCell>Delete</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {
                                    companies.map((company) =>
                                    (
                                        <Row key={ company.id } company={ company } />
                                    ))
                                }
                            </TableBody>
                        </Table>
                    </CardContent>
                </Card>
            </Grid>
            <Grid item sm={ 0 } md={ 1 } lg={ 2 }>

            </Grid>
        </Grid>
    );
}

export default App;
