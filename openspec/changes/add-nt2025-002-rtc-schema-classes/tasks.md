# Plano — Classes RTC (PL_010e_v1.01) no DFe.NET

> Padrão: propriedades na ordem do XSD (`[XmlElement(Order)]` onde a classe já usa; ordem de
> declaração onde não usa) + `ShouldSerialize*`/`*Specified` para `minOccurs=0`. Decimais via
> `.Arredondar(n)`. Fonte: `PL_010e_v1.01`. Comentários pt-BR.

## 1. Grupo Imposto Seletivo (IS / TIS) — ✅ JÁ EXISTIA
- [x] 1.1 `IS.cs` **já existe** em `NFe.Classes/Informacoes/Detalhe/Tributacao/Federal/IS.cs`
  (`CSTIS`, `cClassTribIS`, `vBCIS`, `pIS`, `pISEspec`≈adRemIS, `uTrib`, `qTrib`, `vIS`) e já está
  **encaixado** em `imposto.cs` (`public IS IS`). **Nada a implementar** — escopo removido após
  inspeção do código. (Consumidor product-invoice#265 Fase 4.1 pode emitir gIS sem mudança no Zeus.)

## 2. cIndOp — ✅ FEITO
- [x] 2.1 `cIndOp` adicionado ao grupo **`ide`** (`ide.cs`, B25d — entre `indIntermed` e `procEmi`),
  opcional, com `ShouldSerializecIndOp()`. (Correção de escopo: é do `ide`, não do `prod`.)

## 3. refDFeAnt — ✅ FEITO
- [x] 3.1 `refDFeAnt` (`List<string>`, até 99) adicionado ao `gCompraGov.cs` (tipo XSD
  `TCompraGovReduzido`: tpEnteGov/pRedutor/tpOperGov/**refDFeAnt**), com `ShouldSerializerefDFeAnt()`.

## 4. gALCZFMCBS — ✅ FEITO
- [x] 4.1 `gALCZFMCBS.cs` criado com **`pAliqEfetRegCBS`** + **`vTribRegCBS`** (o `TALCZFMCBS` do
  PL_010e tem só esses 2 campos — correção de escopo; não há tpALCZFMCBS/nProcSUFRAMA neste XSD).
- [x] 4.2 Encaixado no `gCBS.cs` (Order 5, entre `gRed` e `vCBS`; `vCBS` renumerado para Order 6).

## 5. Testes / validação
- [x] 5.3 Build do `NFe.Classes` (Release) sem erros.
- [ ] 5.1/5.2 Round-trip de serialização (nota sem grupos = XML idêntico; com grupos = ordem correta)
  — recomendado adicionar ao projeto de testes do Zeus (follow-up).

## 6. OpenSpec
- [x] 6.1 `openspec validate add-nt2025-002-rtc-schema-classes --strict` sem erros.
