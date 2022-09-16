export interface ListaCitasI{
    id:number,
    date:string,
    time:string,
    attendedClientID:number,
    licensePlate:string,
    branchID:number;
    requiredService:number,
    mechanicID:number,
    assistantID:number,
    necessaryParts:number[]
}