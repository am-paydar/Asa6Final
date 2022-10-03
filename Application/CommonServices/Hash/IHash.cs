namespace Application.CommonServices.Hash
{
    public interface IHash
    {
        string EncodingTxT(string inputTxt);
        string DecodingTxT(string inputTxT);
    }
}
