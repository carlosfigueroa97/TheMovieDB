using System.Threading.Tasks;
using TheMovieDB.Models.App;
using TheMovieDB.ViewModels.Base;

namespace TheMovieDB.ViewModels.Detail
{
    public class MovieDetailPageViewModel : BaseViewModel<ResultWrapper>
    {
        #region Private Properties

        private ResultWrapper _parameter;

        #endregion

        #region Public Properties

        public ResultWrapper Parameter
        {
            get => _parameter;
            set => SetProperty(ref _parameter, value);
        }

        #endregion

        #region Constructor

        public MovieDetailPageViewModel()
        {
        }

        #endregion

        #region Init

        public override Task Init(ResultWrapper parameter)
        {
            Parameter = parameter;

            return base.Init(parameter);
        }

        #endregion
    }
}
