import { Button, Card, CardContent, IconButton, Table, TableBody, TableCell, TableHead, TableRow } from "@mui/material";
import Add from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
import "./App.css";
import { useSnapshot } from "valtio";
import { appState } from "./store/AppState";

// Add a company to the global state.
async function addCompany()
{
    await appState.addRandomCompany();
}

function App()
{
    const { companies, deleteCompany } = useSnapshot(appState);

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
                                <TableCell>ID</TableCell>
                                <TableCell>Company</TableCell>
                                <TableCell>Delete</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {
                                companies.map((company) =>
                                (
                                    <TableRow key={ company.id }>
                                        <TableCell>{ company.id }</TableCell>
                                        <TableCell>{ company.label }</TableCell>
                                        <TableCell>
                                            <IconButton onClick={ async () => await deleteCompany(company) }>
                                                <DeleteIcon />
                                            </IconButton>
                                        </TableCell>
                                    </TableRow>
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
