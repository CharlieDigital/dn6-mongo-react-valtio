import "./App.css";
import { Box, Button, Card, CardContent, Collapse, IconButton, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material";
import Add from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import { useSnapshot } from "valtio";
import React, { useEffect, useState } from "react";
import { appState } from "./store/AppState";
import { Company } from "./services";

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
                <TableCell>{ company.id }</TableCell>
                <TableCell>{ company.label }</TableCell>
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
                                    onClick={ async() => await appState.addEmployeeTo(company) }>
                                    Add Employee
                                </Button>
                            </Typography>
                            <Table size="small">
                                <TableHead>
                                    <TableRow>
                                        <TableCell>FirstName</TableCell>
                                        <TableCell>LastName</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {
                                        company.employees?.map((employee) =>
                                        (
                                            <TableRow key={ employee.id }>
                                                <TableCell>{ employee.firstName }</TableCell>
                                                <TableCell>{ employee.lastName }</TableCell>
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
        <div>
            <Card
                sx={{ mx: "auto", my: 4, width: 720 }}
                variant="outlined">
                <CardContent>
                    Click the button to add a randomly generated Company.
                    <Button
                        sx={{ ml: 4 }}
                        variant="outlined"
                        size="small"
                        startIcon={ <Add/> }
                        onClick={ async () => await addCompany() }>
                        Add Company
                    </Button>
                </CardContent>
                <CardContent>
                    Showing { companies.length } companies.

                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>+</TableCell>
                                <TableCell>ID</TableCell>
                                <TableCell>Company</TableCell>
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
        </div>
    );
}

export default App;
