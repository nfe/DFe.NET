# Plano — Classes RTC (PL_010e_v1.01) no DFe.NET

> Padrão: POCO + `[XmlElement]` na ordem do XSD + `ShouldSerialize*` para `minOccurs=0`.
> Fonte: `PL_010e_v1.01` (`DFeTiposBasicos_v1.00.xsd`, `leiauteNFe_v4.00.xsd`). Comentários pt-BR.

## 1. Grupo Imposto Seletivo (IS / TIS)
- [ ] 1.1 Criar `IS.cs` (`.../Tributacao/Compartilhado/IS/`): `CSTIS` (TCST), `cClassTribIS?`, `vBCIS`, `pIS`, `adRemIS?`, `uTrib?`, `qTrib`, `vIS`, na ordem do XSD, com `ShouldSerialize*` nos opcionais.
- [ ] 1.2 Encaixar `IS` em `imposto.cs` (`det/imposto/IS`, opcional) como irmão de `IBSCBS`.

## 2. cIndOp (produto)
- [ ] 2.1 Adicionar `cIndOp` (opcional) ao `produto` (`prod`), com `ShouldSerializecIndOp()`.

## 3. refDFeAnt (Compras Governamentais)
- [ ] 3.1 Adicionar `refDFeAnt` (`TChDFeRTC`, string) ao grupo de Compras Governamentais (`gCompraGov`), opcional.

## 4. gALCZFMCBS (ALC/ZFM CBS)
- [ ] 4.1 Criar `gALCZFMCBS.cs`: `tpALCZFMCBS`, `nProcSUFRAMA`, `pAliqEfetRegCBS`, `vTribRegCBS`, ordem do XSD.
- [ ] 4.2 Encaixar `gALCZFMCBS` no grupo CBS (IBS/CBS), opcional.

## 5. Testes / validação
- [ ] 5.1 Round-trip de serialização: nota SEM os grupos novos → XML idêntico ao atual (nenhuma tag nova).
- [ ] 5.2 Serialização COM cada grupo → tags na ordem correta e apenas os campos preenchidos.
- [ ] 5.3 Build da solução Zeus (NFe.Classes) sem erros.

## 6. OpenSpec
- [ ] 6.1 `openspec validate add-nt2025-002-rtc-schema-classes --strict` sem erros.
