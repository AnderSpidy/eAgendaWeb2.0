using AutoMapper;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.webapi.Config.AutoMapperConfig.ModuloCompartilhado;
using eAgenda.webapi.ViewModels.ModuloTarefa;

namespace eAgenda.webapi.Config.AutoMapperConfig
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            //-----------------------------------------------------------------------------------------------------------------------------
            //da Entidade para o ViewModel
            ConverterDeEntidadeParaViewModel();
            //-----------------------------------------------------------------------------------------------------------------------------
            //do ViewModela para Entidade
            ConverterDeViewModelParaEntidade();
        }

        private void ConverterDeViewModelParaEntidade()
        {
            CreateMap<InserirTarefaViewModel, Tarefa>()
                            .ForMember(destino => destino.UsuarioId, opt => opt.MapFrom<UsuarioResolver>())//SERVE PARA ENXUGAR O CODIGO DOS CONTROLLER E NAO PRECISAR MAIS FICAR INCLUINDO O USUARIO, MAS SIM DIRETO DELO MAPEADOR
                            .ForMember(destino => destino.Itens, opt => opt.Ignore())
                            .AfterMap<AdicionarItensMappingAction>();//O ADICIONARITENSMAPPINGACTION ESTA APENAS PARA ENXUGAR O CODIGO ABAIXO EM UMA CLASSE
            //.AfterMap((viewModel, tarefa) =>
            //{
            //    if (viewModel.Itens == null)
            //        return;

            //    foreach (var itemVM in viewModel.Itens)
            //    {
            //        var item = new ItemTarefa();

            //        item.Titulo = itemVM.Titulo;

            //        tarefa.AdicionarItem(item);
            //    }
            //});

            CreateMap<EditarTarefaViewModel, Tarefa>()
                .ForMember(destino => destino.Itens, opt => opt.Ignore())
                .AfterMap<EditarItensMappingAction>();
            //.AfterMap((viewModel, tarefa) =>
            //{
            //    foreach (var itemVM in viewModel.Itens)
            //    {
            //        if (itemVM.Concluido)
            //        {
            //            tarefa.ConcluirItem(itemVM.Id);
            //        }
            //        else
            //        {
            //            tarefa.MarcarPendente(itemVM.Id);
            //        }
            //    }

            //    foreach (var itemVM in viewModel.Itens)
            //    {
            //        if (itemVM.Status == StatusItemTarefa.Adicionado)
            //        {
            //            var item = new ItemTarefa(itemVM.Titulo);
            //            tarefa.AdicionarItem(item);
            //        }
            //        else if (itemVM.Status == StatusItemTarefa.Removido)
            //        {
            //            tarefa.RemoverItem(itemVM.Id);

            //        }
            //    }
            //});
        }

        private void ConverterDeEntidadeParaViewModel()
        {
            CreateMap<Tarefa, ListarTarefaViewModel>()
                            .ForMember(destino => destino.Prioridade, opt => opt.MapFrom(origem => origem.Prioridade.GetDescription()))
                            .ForMember(destino => destino.Situacao, opt =>
                                        opt.MapFrom(origem => origem.PercentualConcluido == 100 ? "Concluido" : "Pendente"));

            CreateMap<Tarefa, VisualizarTarefaViewModel>()
                .ForMember(destino => destino.Prioridade, opt => opt.MapFrom(origem => origem.Prioridade.GetDescription()))
                .ForMember(destino => destino.Situacao, opt =>
                            opt.MapFrom(origem => origem.PercentualConcluido == 100 ? "Concluido" : "Pendente"))
                .ForMember(destino => destino.QuantidaDeItens, opt => opt.MapFrom(origem => origem.Itens.Count));

            CreateMap<ItemTarefa, VisualizarItemTarefaViewModel>()
                .ForMember(destino => destino.Situacao, opt =>
                            opt.MapFrom(origem => origem.Concluido ? "Concluido" : "Pendente"));
        }
    }
    public class AdicionarItensMappingAction : IMappingAction<InserirTarefaViewModel, Tarefa>
    {
        public void Process(InserirTarefaViewModel viewModel, Tarefa tarefa, ResolutionContext context)
        {
            if (viewModel.Itens == null)
                return;

            foreach (var itemVM in viewModel.Itens)
            {
                var item = new ItemTarefa();

                item.Titulo = itemVM.Titulo;

                tarefa.AdicionarItem(item);
            }
        }
    }

    public class EditarItensMappingAction : IMappingAction<EditarTarefaViewModel, Tarefa>
    {
        public void Process(EditarTarefaViewModel viewModel, Tarefa tarefa, ResolutionContext context)
        {
            foreach (var itemVM in viewModel.Itens)
            {
                if (itemVM.Concluido)
                    tarefa.ConcluirItem(itemVM.Id);

                else
                    tarefa.MarcarPendente(itemVM.Id);
            }

            foreach (var itemVM in viewModel.Itens)
            {
                if (itemVM.Status == StatusItemTarefa.Adicionado)
                {
                    var item = new ItemTarefa(itemVM.Titulo);
                    tarefa.AdicionarItem(item);
                }
                else if (itemVM.Status == StatusItemTarefa.Removido)
                {
                    tarefa.RemoverItem(itemVM.Id);
                }
            }
        }
    }
}
