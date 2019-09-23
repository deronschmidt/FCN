export class Community {
    ID : number;
    CommunityID : number;
    CommunityName : string;
    Affiliation : string;
    Address1 : string;
    Address2 : string;
    City : string;
    State : string;
    ZipCode : string;
    Phone : string;
    AlternatePhone : string;
    Email : string;
    Website : string;
    Active : boolean;
    Contacts : CommunityContact[];
    CreatedDate : Date;
    UpdatedDate : Date;
}

export class CommunityContact {
    ID : number;
    CommunityID : number;
    CommunityName : string;
    ContactID : number;
    ContactName : string;
    CreatedDate : Date;
    UpdatedDate : Date;
}