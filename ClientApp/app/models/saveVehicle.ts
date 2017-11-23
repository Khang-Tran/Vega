import {IKeyValuePair} from './keyValuePair';
import {IContact} from './contact';

export interface ISaveVehicle {
    id: number;
    modelId: number;
    makeId: number;
    isRegister: boolean;
    features: number[];
    contact: IContact;
}