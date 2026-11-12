# Design — Classes RTC (PL_010e_v1.01) no DFe.NET

## Fonte da verdade
Schema `PL_010e_v1.01`:
- `NFe/DFeTiposBasicos_v1.00.xsd` — tipos RTC (`TIS`, `TChDFeRTC`, `gALCZFMCBS`, decimais `TDec*RTC`).
- `NFe/leiauteNFe_v4.00.xsd` — `cIndOp` no produto; `IS`/`IBSCBS` em `imposto`.

## Decisões

### D1 — Mapear 1:1 o XSD, no padrão da lib
Cada grupo vira POCO em `NFe.Classes/Informacoes/...` com propriedades anotadas
(`[XmlElement("<tag>")]`), na **ordem do XSD**, e `ShouldSerialize<Prop>()` para campos
`minOccurs=0` (não emitir tag quando nulo) — mesmo padrão de `gIBSCBS`/`gIBSCBSMono` já existentes.
Decimais usam os tipos/format já convencionados na lib (string formatada por cultura invariante),
espelhando `IBSCBS.cs`.

### D2 — Colocação das classes
- **IS (TIS):** `Informacoes/Detalhe/Tributacao/Compartilhado/IS/IS.cs`, referenciado por
  `imposto.cs` como irmão de `IBSCBS` (`det/imposto/IS`). Campos: `CSTIS`, `cClassTribIS?`,
  `vBCIS`, `pIS`, `adRemIS?`, `uTrib?`, `qTrib`, `vIS`.
- **gALCZFMCBS:** sob `.../InformacoesIbsCbs/`, referenciado pelo grupo IBS/CBS (CBS).
  Campos: `tpALCZFMCBS`, `nProcSUFRAMA`, `pAliqEfetRegCBS`, `vTribRegCBS`.
- **cIndOp:** propriedade no `produto` (`prod`), opcional.
- **refDFeAnt:** no grupo de Compras Governamentais (`gCompraGov`), tipo `TChDFeRTC` (string 44).

### D3 — Escopo limitado ao PL_010e_v1.01 (nível v1.40)
O XSD fornecido **não** contém o monofásico reformulado v1.50 (Ad Rem × Ad Valorem,
`gpBioDiferenca`) nem `ISUFemit`. Estes ficam **fora** — modelá-los exigiria elementos
inexistentes no schema. Serão tratados numa change futura quando o PL v1.50 oficial chegar.

### D4 — Não quebrar serialização atual
Todos os campos novos são `minOccurs=0`. Com `ShouldSerialize*` retornando `false` quando nulo,
notas sem RTC produzem XML idêntico ao atual. Validar via round-trip de serialização.

## Riscos
- ⚠️ Tipos decimais RTC (`TDec1302RTC`, `TDec_0302_04RTC`, `TDec_1104OpRTC`) — usar o mesmo
  padrão de formatação já aplicado nas classes IBS/CBS existentes; conferir casas decimais.
- ⚠️ Ordem dos elementos no XML deve seguir o XSD (serialização posicional).
